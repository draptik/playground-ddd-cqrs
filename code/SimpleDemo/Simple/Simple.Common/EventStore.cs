using System;
using System.Collections.Generic;
using Simple.Common.Exceptions;

namespace Simple.Common
{
    public class EventStore : IEventStore
    {
        public void CreateNewStream(string streamName, IEnumerable<DomainEvent> domainEvents)
        {
            var eventStream = new EventStream(streamName);
            //_documentSession.Store(eventStream);

            AppendEventsToStream(streamName, domainEvents);
        }

        public void AppendEventsToStream(string streamName, IEnumerable<DomainEvent> domainEvents,
            int? expectedVersion = null)
        {
            //var stream = _documentSession.Load<EventStream>(streamName);

            if (expectedVersion != null)
            {
                //CheckForConcurrencyError(expectedVersion, stream);
            }

            foreach (var @event in domainEvents)
            {
                //_documentSession.Store(stream.RegisterEvent(@event));
            }
        }

        public IEnumerable<DomainEvent> GetStream(string streamName, int fromVersion, int toVersion)
        {
            // Get events from a specific version
            //var eventWrappers = (from stream in _documentSession.Query<EventWrapper>()
            //                      .Customize(x => x.WaitForNonStaleResultsAsOfNow())
            //                     where stream.EventStreamId.Equals(streamName)
            //                     && stream.EventNumber <= toVersion
            //                     && stream.EventNumber >= fromVersion
            //                     orderby stream.EventNumber
            //                     select stream).ToList();

            //if (eventWrappers.Count() == 0) return null;

            var events = new List<DomainEvent>();

            //foreach (var @event in eventWrappers)
            //{
            //    events.Add(@event.Event);
            //}

            return events;
        }

        public void AddSnapshot<T>(string streamName, T snapshot)
        {
            var wrapper = new SnapshotWrapper
            {
                StreamName = streamName,
                Snapshot = snapshot,
                Created = DateTime.Now
            };

            //_documentSession.Store(snapshot);
        }

        public T GetLatestSnapshot<T>(string streamName) where T : class
        {
            //var latestSnapshot = _documentSession.Query<SnapshotWrapper>()
            //                .Customize(x => x.WaitForNonStaleResultsAsOfNow())
            //                .Where(x => x.StreamName == streamName)
            //                .OrderByDescending(x => x.Created)
            //                .FirstOrDefault();

            //if (latestSnapshot == null)
            //{
            //    return null;
            //}
            //else
            //{
            //    return (T)latestSnapshot.Snapshot;
            //}

            return null;
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
    }
}