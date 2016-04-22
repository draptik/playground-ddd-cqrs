using System;
using Simple.Common;

namespace Simple.Domain
{
    public class Customer : EventSourcedAggregate
    {
        public Customer() {}

        public Customer(CustomerSnapshot snapshot)
        {
            InitialVersion = snapshot.Version;
            Version = snapshot.Version;
            Name = snapshot.Name;
            Address = snapshot.Address;
            Id = snapshot.Id;
        }

        public Customer(string name, string address)
        {
            Causes(new CustomerCreated(Guid.NewGuid(), name, address));
        }

        public Customer(Guid aggregateId, string address)
        {
            Causes(new CustomerAddressChanged(aggregateId, address));
        }

        public int InitialVersion { get; }

        public string Name { get; set; }
        public string Address { get; set; }

        public CustomerSnapshot GetCustomerSnapshot()
        {
            return new CustomerSnapshot
            {
                Id = Id,
                Version = Version,
                Name = Name,
                Address = Address
            };
        }


        private void Causes(DomainEvent @event)
        {
            Changes.Add(@event);
            Apply(@event);
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic) @event);
        }

        private void When(CustomerCreated customerCreated)
        {
            Id = customerCreated.AggregateId;
            Name = customerCreated.Name;
            Address = customerCreated.Address;
        }

        private void When(CustomerAddressChanged customerAddressChanged)
        {
            Id = customerAddressChanged.AggregateId;
            Address = customerAddressChanged.Address;
            Version = customerAddressChanged.Version;
        }
    }

    public class CustomerSnapshot
    {
        public int Version { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid Id { get; set; }
    }
}