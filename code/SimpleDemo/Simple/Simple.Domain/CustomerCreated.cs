using System;
using Simple.Common;

namespace Simple.Domain
{
    public class CustomerCreated : DomainEvent
    {
        public CustomerCreated(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}