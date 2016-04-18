using System;
using Simple.Messages;

namespace Simple.CommandStack.Requests
{
    public class GetHistoryForCustomerRequest : IGetHistoryForCustomerRequest
    {
        public Guid CustomerId { get; set; }
        public Guid RequestId { get; set; }
    }
}