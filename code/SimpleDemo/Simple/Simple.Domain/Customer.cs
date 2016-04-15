using System;
using Simple.Common;

namespace Simple.Domain
{
    public class Customer : EventSourcedAggregate
    {
        public Customer()
        {
        }

        public Customer(CustomerSnapshot snapshot)
        {
            Version = snapshot.Version;
            Name = snapshot.Name;
            Address = snapshot.Address;
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

        public CustomerSnapshot GetCustomerSnapshot()
        {
            return new CustomerSnapshot { Version = Version, Name = Name, Address = Address };
        }


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
            this.Id = customerCreated.AggregateId;
            this.Name = customerCreated.Name;
            this.Address = customerCreated.Address;
        }

        private void When(CustomerAddressChanged customerAddressChanged)
        {
            this.Id = customerAddressChanged.AggregateId;
            this.Address = customerAddressChanged.Address;
            this.Version = customerAddressChanged.Version;
        }
    }

    public class CustomerSnapshot
    {
        public int Version { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}