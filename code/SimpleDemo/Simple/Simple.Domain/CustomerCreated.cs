using System;
using Simple.Common;

namespace Simple.Domain
{
    public class CustomerCreated : DomainEvent
    {
        public CustomerCreated(Guid aggregateId, string name, string address) : base(aggregateId)
        {
            Name = name;
            Address = address;
        }

        public string Address { get; }
        public string Name { get; }
    }
}