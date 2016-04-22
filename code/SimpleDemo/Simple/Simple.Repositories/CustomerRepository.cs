using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var expectedVersion = GetExpectedVersion(customer.InitialVersion);
            _eventStore.AppendEventsToStream(streamName, customer.Changes, expectedVersion);
        }

        public Customer FindById(Guid customerId)
        {
            var fromEventNumber = 0;
            const int toEventNumber = int.MaxValue;

            var snapshot = _eventStore.GetLatestSnapshot<CustomerSnapshot>(StreamNameFor(customerId));
            if (snapshot != null) {
                fromEventNumber = snapshot.Version + 1; // load only events after snapshot
            }

            var domainEvents = _eventStore.GetStream(StreamNameFor(customerId), fromEventNumber, toEventNumber);

            Customer customer = null;

            customer = snapshot != null
                ? new Customer(snapshot)
                : new Customer();

            foreach (var @event in domainEvents.OrderBy(x => x.Version)) {
                customer.Apply(@event);
            }

            return customer;
        }

        public IEnumerable<Guid> GetAllIds()
        {
            return _eventStore.GetAllStreamIds();
        }

        public void SaveSnapshot(CustomerSnapshot snapshot, Customer customer)
        {
            var previousSnapshot = GetLatestSnapshot(customer.Id);
            if (previousSnapshot == null || previousSnapshot.Version < snapshot.Version) {
                _eventStore.AddSnapshot(StreamNameFor(customer.Id), snapshot);
            }
        }

        public CustomerSnapshot GetLatestSnapshot(Guid customerId)
        {
            return _eventStore.GetLatestSnapshot<CustomerSnapshot>(StreamNameFor(customerId));
        }

        public async Task<IEnumerable<HistoryItem>> GetHistoryForCustomer(Guid customerId)
        {
            var history = await _eventStore.GetHistoryForAggregate(StreamNameFor(customerId), fromVersion: 0, toVersion: int.MaxValue);
            return history;
        }

        private string StreamNameFor(Guid id)
        {
            return $"{typeof(Customer).Name}@{id}";
        }

        private int? GetExpectedVersion(int expectedVersion)
        {
            if (expectedVersion == 0) {
                // first time the aggregate is stored, there is no expected version
                return null;
            }
            return expectedVersion;
        }
    }
}