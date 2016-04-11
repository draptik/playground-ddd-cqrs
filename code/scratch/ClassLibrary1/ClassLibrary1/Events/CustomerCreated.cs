using System;
using ClassLibrary1.Framework;

namespace ClassLibrary1.Events
{
    public class CustomerCreated : DomainEvent
    {
        public CustomerCreated(Guid id, string name, string address) : base(id)
        {
            this.CustomerId = Guid.NewGuid();
            this.Name = name;
            this.Address = address;
        }

        public Guid CustomerId { get; set; }
        public string Name { get; }
        public string Address { get; }
    }
}