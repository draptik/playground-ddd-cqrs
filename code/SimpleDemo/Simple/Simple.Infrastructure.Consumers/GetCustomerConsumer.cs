using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Requests;
using Simple.Contracts;

namespace Simple.Infrastructure.Consumers
{
    public class GetCustomerConsumer : IConsumer<GetCustomerRequest>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerConsumer(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GetCustomerRequest> context)
        {
            await Task.Run(() =>
            {
                var customer = _repository.FindById(context.Message.CustomerId);
                return customer;
            });
        }
    }
}