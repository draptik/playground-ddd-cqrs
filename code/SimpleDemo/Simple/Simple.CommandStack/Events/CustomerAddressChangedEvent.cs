using System;

namespace Simple.CommandStack.Events
{
    public class CustomerAddressChangedEvent : ICustomerAddressChangedEvent
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
    }
}