using System;
using Simple.Messages;

namespace Simple.CommandStack.Requests
{
    public class ChangeCustomerAddressRequest : IChangeCustomerAddressRequest
    {
        public ChangeCustomerAddressRequest(Guid requestId, Guid customerId, string address)
        {
            Id = requestId;
            Timestamp = DateTime.UtcNow;
            CustomerId = customerId;
            Address = address;
        }

        public Guid Id { get; }
        public Guid CustomerId { get; }

        public string Address { get; }

        public DateTime Timestamp { get; }
    }
}