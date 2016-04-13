using System;

namespace Simple.CommandStack.Requests
{
    public class GetCustomerRequest : IGetCustomerRequest
    {
        public GetCustomerRequest(Guid requestId, Guid customerId)
        {
            RequestId = requestId;
            CustomerId = customerId;
            Timestamp = DateTime.UtcNow;
        }

        public Guid RequestId { get; }
        public Guid CustomerId { get; }
        public DateTime Timestamp { get; }
    }
}