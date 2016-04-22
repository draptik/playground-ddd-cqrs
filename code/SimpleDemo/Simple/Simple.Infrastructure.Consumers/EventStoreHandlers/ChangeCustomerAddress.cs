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
    public class ChangeCustomerAddress : IConsumer<IChangeCustomerAddressRequest>
    {
        private readonly ICustomerRepository _repository;

        public ChangeCustomerAddress(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<IChangeCustomerAddressRequest> context)
        {
            try
            {
                var customer = Convert(context.Message);
                _repository.Save(customer);

                await context.Publish<ICustomerAddressChangedEvent>(new CustomerAddressChangedEvent
                {
                    Id = customer.Id,
                    Address = customer.Address
                });

                await context.RespondAsync(new ChangeCustomerAddressResponse
                {
                    ResponseId = context.Message.CustomerId,
                    Message = "OK"
                });
            }
            catch (Exception exc)
            {
                await
                    context.RespondAsync(new ChangeCustomerAddressResponse
                    {
                        ResponseId = context.Message.CustomerId,
                        Message = "Failed" + Environment.NewLine + exc.Message
                    });
            }
        }

        private Customer Convert(IChangeCustomerAddressRequest request)
        {
            return new Customer(request.CustomerId, request.Address);
        }
    }
}