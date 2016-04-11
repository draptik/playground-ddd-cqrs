using Simple.Domain;

namespace Simple.Contracts
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Save(Customer customer);
    }
}