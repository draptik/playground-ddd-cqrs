using System;

namespace Simple.CommandStack.Events
{
    public interface ICustomerCreatedEvent
    {
        Guid Id { get; set; } 
        string Name { get; set; } 
        string Address { get; set; } 
    }
}