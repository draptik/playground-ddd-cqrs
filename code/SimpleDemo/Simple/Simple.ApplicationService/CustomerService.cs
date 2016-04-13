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
        private readonly Uri serviceAddress;

        public CustomerService(IBusControl bus)
        {
            this._bus = bus;

            var useInMemoryBus = bool.Parse(ConfigurationManager.AppSettings["UseInMemoryBus"]);
            this.serviceAddress = useInMemoryBus
                ? new Uri(ConfigurationManager.AppSettings["ServiceAddressInMemory"])
                : new Uri(ConfigurationManager.AppSettings["ServiceAddress"]);
        }

        public async Task<CreateCustomerResponse> CreateCustomer(Customer customer)
        {
            var client = this.CreateRequestClient();
            var response = await client.Request(new CreateCustomerRequest(Guid.NewGuid(), customer.Name, customer.Address));
            return response;
        }

        public async Task<ChangeCustomerAddressResponse> ChangeCustomerAddress(Guid customerId, string address)
        {
            var client = this.CreateChangeAddressClient();
            var response = await client.Request(new ChangeCustomerAddressRequest(Guid.NewGuid(), customerId, address));
            return response;
        }
        public async Task<GetCustomerResponse> GetCustomer(Guid customerId)
        {
            var client = this.CreateGetCustomer();
            var response = await client.Request(new GetCustomerRequest(Guid.NewGuid(), customerId));
            return response;
        }
        private IRequestClient<GetCustomerRequest, GetCustomerResponse> CreateGetCustomer()
        {
            var client = this._bus.CreateRequestClient<GetCustomerRequest, GetCustomerResponse>(this.serviceAddress, TimeSpan.FromSeconds(30));
            return client;
        }
        private IRequestClient<CreateCustomerRequest, CreateCustomerResponse> CreateRequestClient()
        {
            var client = this._bus.CreateRequestClient<CreateCustomerRequest, CreateCustomerResponse>(this.serviceAddress, TimeSpan.FromSeconds(10));
            return client;
        }

        private IRequestClient<ChangeCustomerAddressRequest, ChangeCustomerAddressResponse> CreateChangeAddressClient()
        {
            var client =
                this._bus.CreateRequestClient<ChangeCustomerAddressRequest, ChangeCustomerAddressResponse>(this.serviceAddress,
                    TimeSpan.FromSeconds(10));
            return client;
        }
       
    }
}