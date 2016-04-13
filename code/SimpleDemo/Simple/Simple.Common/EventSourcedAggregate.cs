using System.Collections.Generic;
using Newtonsoft.Json;

namespace Simple.Common
{
    public abstract class EventSourcedAggregate : Entity
    {
        public EventSourcedAggregate()
        {
            Changes = new List<DomainEvent>();
        }

        [JsonIgnore]
        public List<DomainEvent> Changes { get; }

        public int Version { get; protected set; }

        public abstract void Apply(DomainEvent changes);
    }
}