using System;
using ClassLibrary1.Events;
using ClassLibrary1.Framework;

namespace ClassLibrary1.Domain
{
    public class Customer : AggregateRoot
    {
        private Customer()
        {
        }

        public string Name { get; private set; }
        public string Address { get; private set; }

        public override void Apply(DomainEvent @event)
        {
            this.Version++;
            When((dynamic) @event);
        }

        private void When(CustomerCreated customerCreated)
        {
            this.Id = Guid.NewGuid();
            this.Name = customerCreated.Name;
            this.Address = customerCreated.Address;
        }

        private void When(CustomerAddressChanged customerAddressChanged)
        {
            this.Address = customerAddressChanged.Address;
        }
    }
}