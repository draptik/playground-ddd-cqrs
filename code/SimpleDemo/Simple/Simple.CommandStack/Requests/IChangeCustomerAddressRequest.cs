using System;

namespace Simple.CommandStack.Requests
{
    public interface IChangeCustomerAddressRequest
    {
        Guid Id { get; }
        Guid CustomerId { get; }
        string Address { get; }
        DateTime Timestamp { get; }
    }
}