using System;
using System.Threading.Tasks;
using Demo.Contracts;
using MassTransit;

namespace Demo.Backend
{
    public class CreateUser : IConsumer<CreateNewUserRequest>
    {
        public Task Consume(ConsumeContext<CreateNewUserRequest> context)
        {
            var foo = context.Message as CreateNewUserRequest;
            var name = foo.Name;
            Console.WriteLine("Name: " + name);
            return Task.Run(() => new CreateNewUserResponse {Message = "Consumer received create command with content: " + name});
        }
    }
}