using System;
using Simple.Common;

namespace Simple.Eventstore
{
    public class EventStream
    {
        public EventStream(Guid id, string type)
        {
            Id = id;
            Type = type;
            Version = 0;
        }

        public EventStream(Guid id, string type, int version)
        {
            Id = id;
            Type = type;
            Version = version;
        }

        public Guid Id { get; }

        public string Type { get; }

        public int Version { get; private set; }

        public EventWrapper RegisterEvent(DomainEvent @event)
        {
            Version++;
            return new EventWrapper(@event, Version, Id);
        }
    }
}