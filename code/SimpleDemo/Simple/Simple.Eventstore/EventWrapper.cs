using System;
using Simple.Common;

namespace Simple.Eventstore
{
    public class EventWrapper
    {
        public EventWrapper(DomainEvent @event, int eventNumber, Guid streamStateId)
        {
            Event = @event;
            Type = @event.GetType().AssemblyQualifiedName;
            EventNumber = eventNumber;
            EventStreamId = streamStateId;
            Id = Guid.NewGuid();
            TimeStampUtc = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public string Type { get; private set; }

        public DomainEvent Event { get; private set; }

        public Guid EventStreamId { get; private set; }

        public int EventNumber { get; private set; }

        public DateTime TimeStampUtc { get; private set; }
    }
}