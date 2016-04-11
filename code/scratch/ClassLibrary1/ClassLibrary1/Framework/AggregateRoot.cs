using System;

namespace ClassLibrary1.Framework
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; set; }
        public int Version { get; set; }

        public abstract void Apply(DomainEvent @event);
    }
}