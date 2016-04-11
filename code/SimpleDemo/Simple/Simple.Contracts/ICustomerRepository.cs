using Simple.Domain;
using System;

namespace Simple.Contracts
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Save(Customer customer);
        Customer FindById(Guid customerId);
    }
}