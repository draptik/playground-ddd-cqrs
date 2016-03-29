using System;

namespace ESDemo.App.Infrastructure
{
    public abstract class DomainEvent
    {
        public DomainEvent(Guid aggregateId)
        {
            this.Id = aggregateId;
        }

        public Guid Id { get; private set; }
    }
}