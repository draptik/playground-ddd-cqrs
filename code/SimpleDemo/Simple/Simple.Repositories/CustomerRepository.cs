using System;
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
            var streamName = StreamNameFor(customer.Id);
            _eventStore.CreateNewStream(streamName, customer.Changes);
        }

        private string StreamNameFor(Guid id)
        {
            return $"{typeof (Customer).Name}@{id}";
        }
    }
}