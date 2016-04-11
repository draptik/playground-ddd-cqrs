using System;
using Simple.Common;

namespace Simple.Domain
{
    public class CustomerAddressChanged : DomainEvent
    {
        public string Address { get; }

        public CustomerAddressChanged(Guid aggregateId, string address) : base(aggregateId)
        {
            Address = address;
        }
    }
}