using System;

namespace Simple.Messages
{
    public interface IGetCustomerRequest
    {
        Guid RequestId { get; }
        Guid CustomerId { get; }
        DateTime Timestamp { get; }
    }
}