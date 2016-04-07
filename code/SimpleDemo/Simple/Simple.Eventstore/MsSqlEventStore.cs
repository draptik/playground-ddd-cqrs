using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Simple.Common;

namespace Simple.Eventstore
{
    public class MsSqlEventStore : IEventStore
    {
        public void CreateNewStream(string streamName, IEnumerable<DomainEvent> domainEvents)
        {
            var strings = streamName.Split('@');
            var type = strings[0];
            var id = strings[1];

            var eventStream = new EventStream(new Guid(id), type);


            var sqlConnection = new SqlConnection("credentials-todo");

            var transaction = sqlConnection.BeginTransaction();
            try
            {
                AddEventStream(eventStream, transaction);
                AddEvents(domainEvents.Select(@event => eventStream.RegisterEvent(@event)).ToList(), transaction);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void AppendEventsToStream(string streamName, IEnumerable<DomainEvent> domainEvents, int? expectedVersion)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DomainEvent> GetStream(string streamName, int fromVersion, int toVersion)
        {
            throw new System.NotImplementedException();
        }

        public void AddSnapshot<T>(string streamName, T snapshot)
        {
            throw new System.NotImplementedException();
        }

        public T GetLatestSnapshot<T>(string streamName) where T : class
        {
            throw new System.NotImplementedException();
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
                    //command.Parameters.AddWithValue("TimeStamp", @event.TimeStamp);
                    command.Parameters.AddWithValue("Payload", @event.Event.ToString()); // TODO Serialize this!!
                    command.Parameters.AddWithValue("EventStreamId", @event.EventStreamId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public static class Queries
    {
        public const string InsertNewEventStream = "INSERT INTO [EventStreams](Id, Type, Version) VALUES (@Id, @Type, @Version)";
        public const string InsertEvents = "INSERT INTO [Events](Id, EventType, Version, Payload, EventStreamId) VALUES (@Id, @EventType, @Version, @Payload, @EventStreamId)";
    }
}