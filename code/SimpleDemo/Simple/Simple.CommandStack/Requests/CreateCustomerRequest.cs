using System;

namespace Simple.CommandStack.Requests
{
    public class CreateCustomerRequest : ICreateCustomerRequest
    {
        public CreateCustomerRequest(Guid id, string name, string address)
        {
            Id = id;
            Address = address;
            Name = name;
        }

        public string Address { get; }
        public string Name { get; }
        public Guid Id { get; }
    }
}