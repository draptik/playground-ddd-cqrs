using System;

namespace Simple.Common
{
    public abstract class DomainEvent
    {
        public DomainEvent(Guid aggregateId)
        {
            Id = aggregateId;
        }

        public Guid Id { get; }
    }
}