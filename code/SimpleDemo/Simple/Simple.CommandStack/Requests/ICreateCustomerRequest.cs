using System;

namespace Simple.CommandStack.Requests
{
    public interface ICreateCustomerRequest
    {
        string Address { get; }
        string Name { get; }
        Guid Id { get; }
    }
}