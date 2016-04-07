using System;
using Simple.Common;

namespace Simple.Domain
{
    public class CustomerCreated : DomainEvent
    {
        public string Address { get; }
        public string Name { get; }

        public CustomerCreated(Guid aggregateId, string name, string address) : base(aggregateId)
        {
            this.Name = name;
            this.Address = address;
        }
    }
}