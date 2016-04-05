using System;
using System.Threading.Tasks;
using Demo.Contracts;
using MassTransit;

namespace Demo.UI.Application
{
    public class UserService
    {
        public async Task<CreateNewUserResponse> CreateNewUser(string name)
        {
            var bus = MyApp.MyBus;
            var client = CreateRequestClient(bus);
            var response = await client.Request(new CreateNewUserRequest("foo"));
            return response;
        }

        private static IRequestClient<CreateNewUserRequest, CreateNewUserResponse> CreateRequestClient(IBusControl busControl)
        {
            var serviceAddress = new Uri("rabbitmq://localhost/test/createuser");
            var client = busControl.CreateRequestClient<CreateNewUserRequest, CreateNewUserResponse>(serviceAddress, TimeSpan.FromSeconds(10));
            return client;
        }
    }
}