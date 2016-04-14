using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Events;
using Simple.CommandStack.Requests;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Domain;

namespace Simple.Infrastructure.Consumers.EventStoreHandlers
{
    /// <summary>
    ///     "You should not be using IBus in a consumer -- if you need to publish events, ConsumeContext implements
    ///     IPublishEndpoint, which is where publishing happens. If you need to send, it also includes ISendEndpointProvider,
    ///     which is how you get a send endpoint. In a consumer, all of the messages sent through this approach include the
    ///     proper correlation identifiers (including the ConversationId and the InitiatorId) so you can trace the messages
    ///     through the system. They also ensure the proper return address is included in the message"
    ///     https://groups.google.com/forum/#!msg/masstransit-discuss/f5hNku1r9CU/3JErodFiBwAJ
    /// </summary>
    public class CreateCustomerConsumer : IConsumer<ICreateCustomerRequest>
    {
        //private readonly IBusControl _bus;
        private readonly ICustomerRepository _repository;

        public CreateCustomerConsumer(ICustomerRepository repository)
        {
            this._repository = repository;
            //this._bus = bus;
        }

        public async Task Consume(ConsumeContext<ICreateCustomerRequest> context)
        {
            try {
                var customer = this.Convert(context.Message);
                this._repository.Add(customer);


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