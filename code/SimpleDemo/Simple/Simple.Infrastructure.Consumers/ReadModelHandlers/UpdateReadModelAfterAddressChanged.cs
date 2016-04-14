using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Events;
using Simple.Contracts;

namespace Simple.Infrastructure.Consumers.ReadModelHandlers
{
    public class UpdateReadModelAfterAddressChanged : IConsumer<ICustomerAddressChangedEvent>
    {
        private readonly ICustomerReadModel _readModel;

        public UpdateReadModelAfterAddressChanged(ICustomerReadModel readModel)
        {
            _readModel = readModel;
        }

        public Task Consume(ConsumeContext<ICustomerAddressChangedEvent> context)
        {
            _readModel.Update(context.Message);
            return Task.FromResult(0);
        }
    }
}