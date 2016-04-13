using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Commands;
using Simple.Contracts;

namespace Simple.Infrastructure.Consumers
{
    public class UpdateReadModelConsumer : IConsumer<IUpdateViewModelCommand>
    {
        private readonly IUpdateCustomerReadModelRepository _repository;

        public UpdateReadModelConsumer(IUpdateCustomerReadModelRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<IUpdateViewModelCommand> context)
        {
            _repository.Update(context.Message);

            // TODO Do we have to return anything here??
            return null;
        }
    }
}