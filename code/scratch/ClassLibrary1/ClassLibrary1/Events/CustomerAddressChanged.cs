using System;
using ClassLibrary1.Framework;

namespace ClassLibrary1.Events
{
    public class CustomerAddressChanged : DomainEvent
    {
        public CustomerAddressChanged(Guid id, string address) : base(id)
        {
            this.Address = address;
        }

        public string Address { get; }
    }
}