using System;
using Simple.Common;

namespace Simple.Eventstore
{
    public class EventStream
    {
        private EventStream()
        {
        }

        public EventStream(Guid id, string type)
        {
            this.Id = id;
            this.Type = type;
            this.Version = 0;
        }

        public EventStream(Guid id, string type, int version)
        {
            this.Id = id;
            this.Type = type;
            this.Version = version;
        }

        public Guid Id { get; }

        public string Type { get; }

        public int Version { get; private set; }

        public EventWrapper RegisterEvent(DomainEvent @event)
        {
            this.Version++;
            return new EventWrapper(@event, this.Version, this.Id);
        }
    }
}