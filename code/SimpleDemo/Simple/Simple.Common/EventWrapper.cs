using System;

namespace Simple.Common
{
    public class EventWrapper
    {
        //public string Id { get; }

        public Guid Id { get; }

        public string Type { get; }

        public DomainEvent Event { get; }

        public Guid EventStreamId { get; }

        public int EventNumber { get; }

        public EventWrapper(DomainEvent @event, int eventNumber, Guid streamStateId, string type)
        {
            Event = @event;
            EventNumber = eventNumber;
            EventStreamId = streamStateId;
            Type = type;
            Id = Guid.NewGuid();
        }
    }
}