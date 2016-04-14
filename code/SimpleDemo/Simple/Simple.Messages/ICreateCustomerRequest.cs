using System;

namespace Simple.Messages
{
    public interface ICreateCustomerRequest
    {
        string Address { get; }
        string Name { get; }
        Guid Id { get; }
    }
}