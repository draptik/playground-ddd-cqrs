using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Events;
using Simple.Contracts;

namespace Simple.Infrastructure.Consumers
{
    public class UpdateCustomerCondensedReadModelConsumer : IConsumer<ICustomerCreatedEvent>
    {
        private readonly IUpdateCustomerCondensedRepository _repository;

        public UpdateCustomerCondensedReadModelConsumer(IUpdateCustomerCondensedRepository repository)
        {
            this._repository = repository;
        }

        public Task Consume(ConsumeContext<ICustomerCreatedEvent> context)
        {
            this._repository.Update(context.Message);
            return Task.FromResult(0);
        }
    }
}