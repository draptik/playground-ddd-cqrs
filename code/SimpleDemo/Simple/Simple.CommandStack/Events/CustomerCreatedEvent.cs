using System;

namespace Simple.CommandStack.Events
{
    public class CustomerCreatedEvent : ICustomerCreatedEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}