using System;
using Simple.Common;

namespace Simple.Domain
{
    public class Customer : EventSourcedAggregate
    {
        public Customer()
        {
        }


        public Customer(string name, string address)
        {
            this.Causes(new CustomerCreated(Guid.NewGuid(), name, address));
        }

        public Customer(Guid aggregateId, string address)
        {
            this.Causes(new CustomerAddressChanged(aggregateId, address));
        }

        public string Name { get; set; }
        public string Address { get; set; }

        private void Causes(DomainEvent @event)
        {
            this.Changes.Add(@event);
            this.Apply(@event);
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic) @event);
        }

        private void When(CustomerCreated customerCreated)
        {
            this.Id = customerCreated.Id;
            this.Name = customerCreated.Name;
            this.Address = customerCreated.Address;
        }

        private void When(CustomerAddressChanged customerAddressChanged)
        {
            this.Id = customerAddressChanged.Id;
            this.Address = customerAddressChanged.Address;
        }
    }
}