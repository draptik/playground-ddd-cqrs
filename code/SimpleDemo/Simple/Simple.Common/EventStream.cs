using System;

namespace Simple.Common
{
    public class EventStream
    {
        private EventStream()
        {
        }

        public EventStream(Guid id, string type)
        {
            Id = id;
            Type = type;
            Version = 0;
        }

        public Guid Id { get; }

        public string Type { get; }

        public int Version { get; private set; }

        public EventWrapper RegisterEvent(DomainEvent @event)
        {
            Version++;
            return new EventWrapper(@event, Version, Id, Type);
        }
    }
}