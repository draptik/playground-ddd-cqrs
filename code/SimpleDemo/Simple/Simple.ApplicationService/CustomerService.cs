using System;
using System.Configuration;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Requests;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Domain;

namespace Simple.ApplicationService
{
    public class CustomerService : ICustomerService
    {
        private readonly IBusControl _bus;

        public CustomerService(IBusControl bus)
        {
            _bus = bus;
        }

        public async Task<CreateCustomerResponse> CreateCustomer(Customer customer)
        {
            var client = CreateRequestClient();
            var response = await client.Request(new CreateCustomerRequest(Guid.NewGuid(), customer.Name, customer.Address));
            return response;
        }

        private IRequestClient<CreateCustomerRequest, CreateCustomerResponse> CreateRequestClient()
        {
            var serviceAddress = new Uri(ConfigurationManager.AppSettings["ServiceAddress"]); // TODO Check service address for in-memory usage...
            var client = _bus.CreateRequestClient<CreateCustomerRequest, CreateCustomerResponse>(serviceAddress,
                TimeSpan.FromSeconds(10));
            return client;
        }
    }
}