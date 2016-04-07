using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Requests;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Domain;

namespace Simple.Infrastructure.Consumers
{
    public class CreateCustomerConsumer : IConsumer<CreateCustomerRequest>
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerConsumer(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CreateCustomerRequest> context)
        {
            try
            {
                _repository.Add(Convert(context.Message));

                await context.RespondAsync(new CreateCustomerResponse
                {
                    ResponseId = context.Message.Id,
                    Message = "OK"
                });
            }
            catch (Exception exc)
            {
                await
                    context.RespondAsync(new CreateCustomerResponse
                    {
                        ResponseId = context.Message.Id,
                        Message = "Failed" + Environment.NewLine + exc.Message
                    });
            }
        }

        private Customer Convert(CreateCustomerRequest createCustomerRequest)
        {
            return new Customer {Name = createCustomerRequest.Name, Address = createCustomerRequest.Address };
        }
    }
}