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

        public EventWrapper(DomainEvent @event, int eventNumber, Guid streamStateId)
        {
            this.Event = @event;
            this.Type = @event.GetType().ToString();
            this.EventNumber = eventNumber;
            this.EventStreamId = streamStateId;
            this.Id = Guid.NewGuid();
        }
    }
}