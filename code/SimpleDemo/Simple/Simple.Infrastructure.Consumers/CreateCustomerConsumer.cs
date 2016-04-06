using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Requests;

namespace Simple.Infrastructure.Consumers
{
    public class CreateCustomerConsumer : IConsumer<CreateCustomerRequest>
    {
        public Task Consume(ConsumeContext<CreateCustomerRequest> context)
        {
            // TODO Send to event store
            
            // TODO create response



            throw new NotImplementedException();
        }
    }
}