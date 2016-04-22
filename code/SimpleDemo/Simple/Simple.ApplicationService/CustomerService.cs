using System;
using System.Threading.Tasks;
using Simple.CommandStack.Requests;
using Simple.CommandStack.Responses;
using Simple.CommandStack.ViewModels;
using Simple.Contracts;
using Simple.Messages;

namespace Simple.ApplicationService
{
    public class CustomerService : ICustomerService
    {
        private readonly IRequestClientCreator requestClientCreator;

        public CustomerService(IRequestClientCreator requestClientCreator)
        {
            this.requestClientCreator = requestClientCreator;
        }

        public async Task<CreateCustomerResponse> CreateCustomer(CreateCustomerViewModel customer)
        {
            var client = this.requestClientCreator.Create<ICreateCustomerRequest, CreateCustomerResponse>();
            var createCustomerRequest = new CreateCustomerRequest(Guid.NewGuid(), customer.Name, customer.Address);
            var response = await client.Request(createCustomerRequest);
            return response;
        }

        public async Task<ChangeCustomerAddressResponse> ChangeCustomerAddress(Guid customerId, string address)
        {
            var client = this.requestClientCreator.Create<IChangeCustomerAddressRequest, ChangeCustomerAddressResponse>();
            var response = await client.Request(new ChangeCustomerAddressRequest(Guid.NewGuid(), customerId, address));
            return response;
        }

        public async Task<GetCustomerResponse> GetCustomer(Guid customerId)
        {
            var client = this.requestClientCreator.Create<IGetCustomerRequest, GetCustomerResponse>();
            var response = await client.Request(new GetCustomerRequest(Guid.NewGuid(), customerId));
            return response;
        }

        public async Task<GetAllCustomersResponse> GetAllCustomers()
        {
            var client = this.requestClientCreator.Create<IGetAllCustomersRequest, GetAllCustomersResponse>();
            var response = await client.Request(new GetAllCustomersRequest());
            return response;
        }
    }
}