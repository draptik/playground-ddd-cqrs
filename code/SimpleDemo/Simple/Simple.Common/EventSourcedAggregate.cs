using System.Collections.Generic;

namespace Simple.Common
{
    public abstract class EventSourcedAggregate : Entity
    {
        public EventSourcedAggregate()
        {
            Changes = new List<DomainEvent>();
        }

        public List<DomainEvent> Changes { get; }

        public int Version { get; protected set; }

        public abstract void Apply(DomainEvent changes);
    }
}