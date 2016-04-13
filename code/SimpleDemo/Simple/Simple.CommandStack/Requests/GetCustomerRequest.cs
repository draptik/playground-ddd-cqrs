using Simple.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Simple.CommandStack.Requests
{
    public class GetCustomerRequest : EventSourcedAggregate
    {
        public Guid RequestId { get;}
        public Guid CustomerId { get; }
        public DateTime Timestamp { get; }
        public GetCustomerRequest(Guid requestId, Guid customerId)
        {
            RequestId = requestId;
            CustomerId = customerId;
            Timestamp = DateTime.UtcNow;
        }
        public override void Apply(DomainEvent changes)
        {
            //nothing to do
        }
    }
}
