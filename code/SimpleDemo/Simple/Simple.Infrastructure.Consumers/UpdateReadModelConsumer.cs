using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Events;
using Simple.Contracts;

namespace Simple.Infrastructure.Consumers
{
    public class UpdateReadModelConsumer : IConsumer<ICustomerCreatedEvent>
    {
        private readonly IUpdateCustomerReadModelRepository _repository;

        public UpdateReadModelConsumer(IUpdateCustomerReadModelRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<ICustomerCreatedEvent> context)
        {
            _repository.Update(context.Message);
            return Task.FromResult(0);
        }
    }
}