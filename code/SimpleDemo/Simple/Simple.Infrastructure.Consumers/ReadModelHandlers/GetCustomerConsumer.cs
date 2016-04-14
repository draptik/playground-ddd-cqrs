using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Requests;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Messages;

namespace Simple.Infrastructure.Consumers.ReadModelHandlers
{
    public class GetCustomerConsumer : IConsumer<IGetCustomerRequest>
    {
        private readonly ICustomerReadModel _readModel;

        public GetCustomerConsumer(ICustomerReadModel readModel)
        {
            _readModel = readModel;
        }

        public async Task Consume(ConsumeContext<IGetCustomerRequest> context)
        {
            try
            {
                await context.RespondAsync(new GetCustomerResponse
                {
                    Customer = _readModel.FindById(context.Message.CustomerId),
                    Message = "ok",
                    ResponseId = context.Message.CustomerId
                });
            }
            catch (Exception exc)
            {
                await
                    context.RespondAsync(new GetCustomerResponse
                    {
                        ResponseId = context.Message.CustomerId,
                        Message = "Failed" + Environment.NewLine + exc.Message
                    });
                throw;
            }
        }
    }
}