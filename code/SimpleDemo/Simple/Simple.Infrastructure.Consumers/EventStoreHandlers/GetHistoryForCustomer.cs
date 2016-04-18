using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Messages;

namespace Simple.Infrastructure.Consumers.EventStoreHandlers
{
    public class GetHistoryForCustomer : IConsumer<IGetHistoryForCustomerRequest>
    {
        private readonly ICustomerRepository _repository;

        public GetHistoryForCustomer(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<IGetHistoryForCustomerRequest> context)
        {
            var historyForCustomer = await _repository.GetHistoryForCustomer(context.Message.CustomerId);

            await context.RespondAsync(new GetHistoryForCustomerResponse
            {
                HistoryItems = historyForCustomer,
                Message = "OK"
            });
        }
    }
}