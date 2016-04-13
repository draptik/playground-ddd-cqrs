using System;
using Simple.CommandStack.Responses;
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

        public void Save(Customer customer)
        {
            var streamName = StreamNameFor(customer.Id);
            // TODO Implement once snapshots are available
            //var expectedVersion = GetExpectedVersion(customer.InitialVersion);
            _eventStore.AppendEventsToStream(streamName, customer.Changes, null);
        }

        public GetCustomerResponse FindById(Guid customerId)
        {
            var domainEvents = _eventStore.GetStream(StreamNameFor(customerId), 0, int.MaxValue);

            var customer = new Customer();

            foreach (var @event in domainEvents)
            {
                customer.Apply(@event);
            }
            return new GetCustomerResponse { Customer = customer, ResponseId = customerId, Message = "ok"};
        }

        private string StreamNameFor(Guid id)
        {
            return $"{typeof (Customer).Name}@{id}";
        }

        private int? GetExpectedVersion(int expectedVersion)
        {
            if (expectedVersion == 0)
            {
                // first time the aggregate is stored, there is no expected version
                return null;
            }
            return expectedVersion;
        }
    }
}