using System;

namespace Simple.CommandStack.Events
{
    public interface ICustomerAddressChangedEvent
    {
        Guid Id { get; set; }
        string Address { get; set; }
    }
}