using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Events;
using Simple.Contracts;

namespace Simple.Infrastructure.Consumers
{
    /// <summary>
    ///     This implementation will never be invoked by the IoC container.
    ///     The IoC container only implements one concrete class per interface by default...
    /// </summary>
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

            // TODO Do we have to return anything here??
            return null;
        }
    }
}