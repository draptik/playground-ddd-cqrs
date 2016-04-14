using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Events;
using Simple.Contracts;

namespace Simple.Infrastructure.Consumers.ReadModelHandlers
{
    public class UpdateReadModelConsumer : IConsumer<ICustomerCreatedEvent>
    {
        private readonly ICustomerReadModel _readModel;

        public UpdateReadModelConsumer(ICustomerReadModel readModel)
        {
            _readModel = readModel;
        }

        public Task Consume(ConsumeContext<ICustomerCreatedEvent> context)
        {
            _readModel.Update(context.Message);
            return Task.FromResult(0);
        }
    }
}