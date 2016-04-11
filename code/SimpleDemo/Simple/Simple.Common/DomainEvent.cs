using System;
using System.Dynamic;

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