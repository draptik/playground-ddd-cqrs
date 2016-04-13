using System;

namespace Simple.Common
{
    public abstract class DomainEvent
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        public Guid AggregateId { get; }

        public int Version { get; set; }
    }
}