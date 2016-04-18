using System;

namespace Simple.Messages
{
    public interface IGetHistoryForCustomerRequest
    {
        Guid CustomerId { get; set; }
        Guid RequestId { get; set; }
    }
}