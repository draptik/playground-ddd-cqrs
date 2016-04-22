using System;
using Simple.Common;

namespace Simple.Domain
{
    public class CustomerAddressChanged : DomainEvent
    {
        public CustomerAddressChanged(Guid aggregateId, string address) : base(aggregateId)
        {
            Address = address;
        }

        public string Address { get; }
    }
}