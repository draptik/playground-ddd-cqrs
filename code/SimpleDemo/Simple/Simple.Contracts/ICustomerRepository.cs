using Simple.Domain;
using System;
using Simple.CommandStack.Responses;

namespace Simple.Contracts
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Save(Customer customer);
        GetCustomerResponse FindById(Guid customerId);
    }
}