using System;
using Simple.Common;

namespace Simple.CommandStack.Requests
{
    public class CreateCustomerRequest : EventSourcedAggregate
    {
        public string Address { get; }
        public string Name { get; }
        public DateTime Timestamp { get; }

        public CreateCustomerRequest(Guid requestId, string name, string address)
        {
            Id = requestId;
            Address = address;
            Name = name;
            Timestamp = DateTime.UtcNow;
        }

        public override void Apply(DomainEvent changes)
        {
            // nothing to do
        }
    }
}