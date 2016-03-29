using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESDemo.App.Infrastructure;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ESDemo.App
{
    public class GetEventStore : IEventStore
    {
        private const string EventClrTypeHeader = "EventClrTypeName";
        private readonly IEventStoreConnection esConn;

        public GetEventStore(IEventStoreConnection esConn)
        {
            this.esConn = esConn;
        }

        public void CreateNewStream(string streamName, IEnumerable<DomainEvent> domainEvents)
        {
            this.AppendEventsToStream(streamName, domainEvents, null);
        }

        public void AppendEventsToStream(string streamName, IEnumerable<DomainEvent> domainEvents, int? expectedVersion)
        {
            var commitId = Guid.NewGuid();

            var eventsInStorageFormat = domainEvents.Select(e => this.MapToEventStoreStorageFormat(e, commitId, e.Id));

            this.esConn.AppendToStreamAsync(this.StreamName(streamName),
                expectedVersion ?? ExpectedVersion.Any,
                eventsInStorageFormat);
        }

        private EventData MapToEventStoreStorageFormat(object evnt, Guid commitId, Guid eventId)
        {
            var headers = new Dictionary<string, object>
            {
                {"CommitId", commitId},
                {EventClrTypeHeader, evnt.GetType().AssemblyQualifiedName}
            };

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evnt));
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(headers));

            var isJson = true;

            return new EventData(eventId, evnt.GetType().Name, isJson, data, metadata);
        }

        public IEnumerable<DomainEvent> GetStream(string streamName, int fromVersion, int toVersion)
        {
            var amount = (toVersion - fromVersion) + 1;
            var events = this.esConn.ReadStreamEventsForwardAsync(this.StreamName(streamName), fromVersion, amount, false).Result;

            return events.Events.Select(e => (DomainEvent)RebuildEvent(e));
        }

        private object RebuildEvent(ResolvedEvent eventStoreEvent)
        {
            var metadata = eventStoreEvent.OriginalEvent.Data;
            var data = eventStoreEvent.OriginalEvent.Data;
            var typeOfDomainEvent = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(EventClrTypeHeader).Value;
            var rebuiltEvent = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string) typeOfDomainEvent));
            return rebuiltEvent;
        }

        public void AddSnapshot<T>(string streamName, T snapshot)
        {
            var stream = this.SnapshotStreamNameFor(streamName);
            var snapshotAsEvent = this.MapToEventStoreStorageFormat(snapshot, Guid.NewGuid(), Guid.NewGuid());
            this.esConn.AppendToStreamAsync(stream, ExpectedVersion.Any, snapshotAsEvent);
        }

        public T GetLatestSnapshot<T>(string streamName) where T : class
        {
            var stream = this.SnapshotStreamNameFor(streamName);
            var amountToFetch = 1;
            var ev = this.esConn.ReadStreamEventsBackwardAsync(stream, StreamPosition.End, amountToFetch, false).Result;

            if (ev.Events.Any()) {
                return (T) this.RebuildEvent(ev.Events.Single());
            }
            return null;
        }

        private string SnapshotStreamNameFor(string streamName)
        {
            return this.StreamName(streamName) + "-snapshots";
        }

        private string StreamName(string streamName)
        {
            var sp = streamName.Split(new[] {'-'}, 2);
            return sp[0] + "-" + sp[1].Replace("-", "");
        }
    }
}