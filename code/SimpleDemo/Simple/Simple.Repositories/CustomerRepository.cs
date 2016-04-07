using Simple.Common;
using Simple.Contracts;
using Simple.Domain;

namespace Simple.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IEventStore _eventStore;

        public CustomerRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Add(Customer customer)
        {
            _eventStore.CreateNewStream("todo", customer.Changes);
        }
    }
}