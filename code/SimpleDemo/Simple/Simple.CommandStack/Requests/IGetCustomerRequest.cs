using System;

namespace Simple.CommandStack.Requests
{
    public interface IGetCustomerRequest
    {
        Guid RequestId { get; }
        Guid CustomerId { get; }
        DateTime Timestamp { get; }
    }
}