using System;

namespace Simple.Messages
{
    public interface IChangeCustomerAddressRequest
    {
        Guid Id { get; }
        Guid CustomerId { get; }
        string Address { get; }
        DateTime Timestamp { get; }
    }
}