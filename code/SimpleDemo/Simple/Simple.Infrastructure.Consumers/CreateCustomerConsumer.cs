using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Events;
using Simple.CommandStack.Requests;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Domain;

namespace Simple.Infrastructure.Consumers
{
    public class CreateCustomerConsumer : IConsumer<ICreateCustomerRequest>
    {
        private readonly ICustomerRepository _repository;
        private readonly IBusControl _bus;

        public CreateCustomerConsumer(ICustomerRepository repository, IBusControl bus)
        {
            this._repository = repository;
            this._bus = bus;
        }

        public async Task Consume(ConsumeContext<ICreateCustomerRequest> context)
        {
            try {
                var customer = this.Convert(context.Message);
                this._repository.Add(customer);

                //await _bus.Publish<ICustomerCreatedEvent>(new CustomerCreatedEvent
                //{
                //    Id = customer.Id,
                //    Name = customer.Name,
                //    Address = customer.Address
                //});

                await context.RespondAsync(new CreateCustomerResponse
                {
                    ResponseId = customer.Id,
                    Message = "OK"
                });
            }
            catch (Exception exc) {
                await
                    context.RespondAsync(new CreateCustomerResponse
                    {
                        ResponseId = context.Message.Id,
                        Message = "Failed" + Environment.NewLine + exc.Message
                    });
            }
        }

        private Customer Convert(ICreateCustomerRequest createCustomerRequest)
        {
            // Ctor created new Guid (derived from Entity)
            return new Customer(createCustomerRequest.Name, createCustomerRequest.Address);
        }
    }
}