using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Events;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Domain;
using Simple.Messages;

namespace Simple.Infrastructure.Consumers.EventStoreHandlers
{
    public class CreateCustomerConsumer : IConsumer<ICreateCustomerRequest>
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerConsumer(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ICreateCustomerRequest> context)
        {
            try {
                var customer = Convert(context.Message);
                _repository.Add(customer);


                await context.Publish<ICustomerCreatedEvent>(new CustomerCreatedEvent
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address
                });

                await context.RespondAsync(new CreateCustomerResponse
                {
                    ResponseId = customer.Id,
                    Message = "OK"
                });
            }
            catch (Exception exc) {
                await context.RespondAsync(new CreateCustomerResponse
                {
                    ResponseId = context.Message.Id,
                    Message = "Failed" + Environment.NewLine + exc.Message
                });
            }
        }

        private Customer Convert(ICreateCustomerRequest createCustomerRequest)
        {
            return new Customer(createCustomerRequest.Name, createCustomerRequest.Address);
        }
    }
}