using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Messages;

namespace Simple.Infrastructure.Consumers.ReadModelHandlers
{
    public class GetAllCustomersConsumer : IConsumer<IGetAllCustomersRequest>
    {
        private readonly ICustomerReadModel _readModel;

        public GetAllCustomersConsumer(ICustomerReadModel readModel)
        {
            _readModel = readModel;
        }

        public async Task Consume(ConsumeContext<IGetAllCustomersRequest> context)
        {
            try {
                await context.RespondAsync(new GetAllCustomersResponse
                {
                    Customers = _readModel.GetAll(),
                    Message = "ok"
                });
            }
            catch (Exception exc) {
                await
                    context.RespondAsync(new GetAllCustomersResponse
                    {
                        Message = "Failed" + Environment.NewLine + exc.Message
                    });
                throw;
            }
        }
    }
}