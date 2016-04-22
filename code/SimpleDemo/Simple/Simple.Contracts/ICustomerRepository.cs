using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Simple.Common;
using Simple.Domain;

namespace Simple.Contracts
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Save(Customer customer);
        Customer FindById(Guid customerId);
        IEnumerable<Guid> GetAllIds();
        void SaveSnapshot(CustomerSnapshot snapshot, Customer customer);
        CustomerSnapshot GetLatestSnapshot(Guid customerId);
        Task<IEnumerable<HistoryItem>> GetHistoryForCustomer(Guid customerId);
    }
}