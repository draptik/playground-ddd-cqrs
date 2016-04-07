using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Simple.Common;

namespace Simple.Eventstore
{
    public class MsSqlEventStore : IEventStore
    {
        public void CreateNewStream(string streamName, IEnumerable<DomainEvent> domainEvents)
        {
            throw new System.NotImplementedException();
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


        private static void AddEventStream(Guid eventStreamId, Type eventStreamType, long initialVersion, SqlTransaction transaction)
        {
            using (var command = new SqlCommand(Queries.InsertNewProviderQuery, transaction.Connection))
            {
                command.Transaction = transaction;
                command.Parameters.AddWithValue("Id", eventStreamId);
                command.Parameters.AddWithValue("Type", eventStreamType.ToString());
                command.Parameters.AddWithValue("Version", initialVersion);
                command.ExecuteNonQuery();
            }
        }
    }

    public static class Queries
    {
        public const string InsertNewProviderQuery = "INSERT INTO [EventStreams](Id, Type, Version) VALUES (@Id, @Type, @Version)";
    }
}