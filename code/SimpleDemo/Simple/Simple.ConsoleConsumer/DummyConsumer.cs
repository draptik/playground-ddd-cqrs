using System;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Requests;
using Simple.Messages;

namespace Simple.ConsoleConsumer
{
    public class DummyConsumer : IConsumer<ICreateCustomerRequest>
    {
        public Task Consume(ConsumeContext<ICreateCustomerRequest> context)
        {
            Console.WriteLine("hallo");

            Console.WriteLine($"Name: {context.Message.Name}");
            
            return Task.FromResult(0);
        }
    }
}