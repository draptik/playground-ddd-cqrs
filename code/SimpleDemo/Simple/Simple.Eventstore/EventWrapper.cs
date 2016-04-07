using System;
using Simple.Common;

namespace Simple.Eventstore
{
    public class EventWrapper
    {
        public Guid Id { get; }

        public string Type { get; }

        public DomainEvent Event { get; }

        public Guid EventStreamId { get; }

        public int EventNumber { get; }

        public EventWrapper(DomainEvent @event, int eventNumber, Guid streamStateId, string type)
        {
            this.Event = @event;
            this.EventNumber = eventNumber;
            this.EventStreamId = streamStateId;
            this.Type = type;
            this.Id = Guid.NewGuid();
        }
    }
}