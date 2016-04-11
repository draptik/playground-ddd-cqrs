using System;

namespace ClassLibrary1.Framework
{
    public abstract class DomainEvent
    {
        protected DomainEvent(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}