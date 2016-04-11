using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Requests;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Domain;

namespace Simple.Infrastructure.Consumers
{
    public class ChangeCustomerAddress : IConsumer<ChangeCustomerAddressRequest>
    {
        private readonly ICustomerRepository _repository;

        public ChangeCustomerAddress(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ChangeCustomerAddressRequest> context)
        {
            try
            {
                _repository.Save(Convert(context.Message));

                await context.RespondAsync(new ChangeCustomerAddressResponse()
                {
                    ResponseId = context.Message.Id,
                    Message = "OK"
                });
            }
            catch (Exception exc)
            {
                await
                    context.RespondAsync(new ChangeCustomerAddressResponse
                    {
                        ResponseId = context.Message.Id,
                        Message = "Failed" + Environment.NewLine + exc.Message
                    });
            }
        }

        private Customer Convert(ChangeCustomerAddressRequest request)
        {
            // Maybe use a separate DTO (instead of 'Customer')?
            return new Customer(request.CustomerId, request.Address);
        }
    }
}