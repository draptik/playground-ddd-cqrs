﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Simple.Common;
using Simple.Common.Exceptions;

namespace Simple.Eventstore
{
    public class MsSqlEventStore : IEventStore
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["SimpleEventStore"].ConnectionString;

        public void CreateNewStream(string streamName, IEnumerable<DomainEvent> domainEvents)
        {
            var eventStream = new EventStream(streamName.Id(), streamName.Type());

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var transaction = sqlConnection.BeginTransaction();
                try
                {
                    AddEventStream(eventStream, transaction);
                    transaction.Commit();
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            var minVersion = 0;
            AppendEventsToStream(streamName, domainEvents, minVersion);
        }

        public void AppendEventsToStream(string streamName, IEnumerable<DomainEvent> domainEvents, int? expectedVersion)
        {
            var eventStream = GetEventStreamMeta(streamName.Id());

            if (expectedVersion != null)
            {
                CheckForConcurrencyError(expectedVersion, eventStream);
            }

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var transaction = sqlConnection.BeginTransaction();
                try
                {
                    AddEvents(domainEvents.Select(@event => eventStream.RegisterEvent(@event)).ToList(), transaction);
                    UpdateStream(eventStream, transaction);

                    transaction.Commit();
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public IEnumerable<DomainEvent> GetStream(string streamName, int fromVersion, int toVersion)
        {
            var domainEvents = new List<DomainEvent>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(Queries.GetEventStream, sqlConnection))
                {
                    command.Parameters.AddWithValue("EventStreamId", streamName.Id());
                    command.Parameters.AddWithValue("MinVersion", fromVersion);
                    command.Parameters.AddWithValue("MaxVersion", toVersion);
                    sqlConnection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //var dbId = reader.GetGuid(reader.GetOrdinal("Id"));
                            var dbType = reader.GetString(reader.GetOrdinal("EventType"));
                            var dbVersion = reader.GetInt32(reader.GetOrdinal("Version"));
                            var dbPayload = reader.GetString(reader.GetOrdinal("Payload"));
                            //var dbTimestampUtc = reader.GetDateTime(reader.GetOrdinal("TimestampUtc"));
                            //var dbEventStreamId = reader.GetGuid(reader.GetOrdinal("EventStreamId"));
                            var payload = JsonConvert.DeserializeObject(dbPayload, Type.GetType(dbType));
                            var domainEvent = payload as DomainEvent;
                            if (domainEvent != null)
                            {
                                domainEvent.Version = dbVersion;
                                domainEvents.Add(domainEvent);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return domainEvents;
        }

        public async Task<IEnumerable<HistoryItem>> GetHistoryForAggregate(string streamName, int fromVersion,
            int toVersion)
        {
            return await Task.Run(() =>
            {
                var historyItems = new List<HistoryItem>();

                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(Queries.GetEventStream, sqlConnection))
                    {
                        command.Parameters.AddWithValue("EventStreamId", streamName.Id());
                        command.Parameters.AddWithValue("MinVersion", fromVersion);
                        command.Parameters.AddWithValue("MaxVersion", toVersion);
                        sqlConnection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var dbType = reader.GetString(reader.GetOrdinal("EventType"));
                                var dbVersion = reader.GetInt32(reader.GetOrdinal("Version"));
                                var dbPayload = reader.GetString(reader.GetOrdinal("Payload"));
                                var dbTimestampUtc = reader.GetDateTime(reader.GetOrdinal("TimestampUtc"));
                                historyItems.Add(new HistoryItem
                                {
                                    DomainEvent = dbPayload,
                                    TimestampUtc = dbTimestampUtc,
                                    Version = dbVersion,
                                    Type = dbType
                                });
                            }
                        }
                        sqlConnection.Close();
                    }
                }

                return historyItems;
            });
        }

        public void AddSnapshot<T>(string streamName, T snapshot)
        {
            var wrapper = new SnapshotWrapper
            {
                StreamName = streamName.Id().ToString(),
                Snapshot = snapshot,
                Created = DateTime.UtcNow
            };

            var eventStreamGuid = new Guid(wrapper.StreamName);

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var transaction = sqlConnection.BeginTransaction();
                try
                {
                    using (var command = new SqlCommand(Queries.InsertSnapshot, transaction.Connection))
                    {
                        command.Transaction = transaction;

                        command.Parameters.AddWithValue("Id", wrapper.Id);
                        command.Parameters.AddWithValue("EventStreamId", eventStreamGuid);
                        command.Parameters.AddWithValue("Payload", JsonConvert.SerializeObject(wrapper.Snapshot));
                        command.Parameters.AddWithValue("CreatedUtc", wrapper.Created);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public TSnapshot GetLatestSnapshot<TSnapshot>(string streamName) where TSnapshot : class
        {
            TSnapshot result = null;

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(Queries.GetLatestSnapshot, sqlConnection))
                {
                    command.Parameters.AddWithValue("EventStreamId", streamName.Id().ToString());
                    sqlConnection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dbPayload = reader.GetString(reader.GetOrdinal("Payload"));
                            result = (TSnapshot) JsonConvert.DeserializeObject(dbPayload, typeof(TSnapshot));
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<Guid> GetAllStreamIds()
        {
            var result = new List<Guid>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(Queries.GetAllEventStreamIds, sqlConnection))
                {
                    sqlConnection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dbId = reader.GetGuid(reader.GetOrdinal("Id"));
                            result.Add(dbId);
                        }
                    }
                }
            }

            return result;
        }

        private static void CheckForConcurrencyError(int? expectedVersion, EventStream stream)
        {
            var lastUpdatedVersion = stream.Version;
            if (lastUpdatedVersion != expectedVersion)
            {
                var error = $"Expected: {expectedVersion}. Found: {lastUpdatedVersion}";
                throw new OptimsticConcurrencyException(error);
            }
        }

        private void UpdateStream(EventStream eventStream, SqlTransaction transaction)
        {
            using (var command = new SqlCommand(Queries.UpdateVersionInEventStream, transaction.Connection))
            {
                command.Transaction = transaction;

                command.Parameters.AddWithValue("Id", eventStream.Id);
                command.Parameters.AddWithValue("Version", eventStream.Version);

                command.ExecuteNonQuery();
            }
        }


        private static void AddEventStream(EventStream eventStream, SqlTransaction transaction)
        {
            using (var command = new SqlCommand(Queries.InsertNewEventStream, transaction.Connection))
            {
                command.Transaction = transaction;

                command.Parameters.AddWithValue("Id", eventStream.Id);
                command.Parameters.AddWithValue("Type", eventStream.Type);
                command.Parameters.AddWithValue("Version", eventStream.Version);

                command.ExecuteNonQuery();
            }
        }

        private static void AddEvents(List<EventWrapper> events, SqlTransaction transaction)
        {
            foreach (var @event in events)
            {
                using (var command = new SqlCommand(Queries.InsertEvents, transaction.Connection))
                {
                    command.Transaction = transaction;

                    command.Parameters.AddWithValue("Id", @event.Id);
                    command.Parameters.AddWithValue("EventType", @event.Type);
                    command.Parameters.AddWithValue("Version", @event.EventNumber);
                    command.Parameters.AddWithValue("Payload", JsonConvert.SerializeObject(@event.Event));
                    command.Parameters.AddWithValue("EventStreamId", @event.EventStreamId);
                    command.Parameters.AddWithValue("TimeStampUtc", @event.TimeStampUtc);

                    command.ExecuteNonQuery();
                }
            }
        }

        private EventStream GetEventStreamMeta(Guid eventStreamId)
        {
            EventStream result = null;

            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(Queries.GetEventStreamMeta, sqlConnection))
            {
                command.Parameters.AddWithValue("Id", eventStreamId);
                sqlConnection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dbId = reader.GetGuid(reader.GetOrdinal("Id"));
                        var dbType = reader.GetString(reader.GetOrdinal("Type"));
                        var dbVersion = reader.GetInt32(reader.GetOrdinal("Version"));

                        result = new EventStream(dbId, dbType, dbVersion);
                    }
                }

                sqlConnection.Close();
            }

            return result;
        }
    }

    public static class Queries
    {
        public const string InsertNewEventStream = "INSERT INTO [EventStreams](Id, Type, Version) " +
            " VALUES (@Id, @Type, @Version)";

        public const string GetEventStreamMeta = "SELECT TOP 1 [Id], [Type], [Version] " +
            " FROM [EventStreams] " +
            " WHERE [Id] = @Id";

        public const string InsertEvents = "INSERT INTO [Events]" +
            "(Id, EventType, Version, Payload, EventStreamId, TimeStampUtc) " +
            " VALUES (@Id, @EventType, @Version, @Payload, @EventStreamId, @TimeStampUtc)";

        public const string GetEventStream =
            "SELECT [Id], [EventType], [Version], [Payload], [EventStreamId], [TimestampUtc] " +
                " FROM [Events]" +
                " WHERE [EventStreamId] = @EventStreamId " +
                " AND [Version] >= @MinVersion " +
                " AND [Version] <= @MaxVersion " +
                " ORDER BY [Version]";

        public const string GetAllEventStreamIds = "SELECT [Id] FROM [EventStreams]";

        public const string UpdateVersionInEventStream = "UPDATE [EventStreams] " +
            " SET [Version] = @Version " +
            " WHERE [Id] = @Id";

        public const string InsertSnapshot = "INSERT INTO [Snapshots](Id, EventStreamId, Payload, CreatedUtc) " +
            " VALUES (@Id, @EventStreamId, @Payload, @CreatedUtc)";

        public const string GetLatestSnapshot = "SELECT TOP(1) * FROM [Snapshots] " +
            " WHERE [EventStreamId] = @EventStreamId " +
            " ORDER BY [CreatedUtc] DESC";
    }
}