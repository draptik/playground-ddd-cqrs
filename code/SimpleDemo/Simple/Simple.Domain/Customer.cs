using Simple.Common;

namespace Simple.Domain
{
    public class Customer : EventSourcedAggregate
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic) @event);
        }

        private void When(CustomerCreated customerCreated)
        {
            Id = customerCreated.Id;
        }
    }
}