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
        private readonly IRequestClientCreator _requestClientCreator;

        public CustomerService(IRequestClientCreator requestClientCreator)
        {
            _requestClientCreator = requestClientCreator;
        }

        public async Task<CreateCustomerResponse> CreateCustomer(CreateCustomerViewModel customer)
        {
            var client = _requestClientCreator.Create<ICreateCustomerRequest, CreateCustomerResponse>();
            var createCustomerRequest = new CreateCustomerRequest(Guid.NewGuid(), customer.Name, customer.Address);
            var response = await client.Request(createCustomerRequest);
            return response;
        }

        public async Task<ChangeCustomerAddressResponse> ChangeCustomerAddress(Guid customerId, string address)
        {
            var client = _requestClientCreator.Create<IChangeCustomerAddressRequest, ChangeCustomerAddressResponse>();
            var response = await client.Request(new ChangeCustomerAddressRequest(Guid.NewGuid(), customerId, address));
            return response;
        }

        public async Task<GetCustomerResponse> GetCustomer(Guid customerId)
        {
            var client = _requestClientCreator.Create<IGetCustomerRequest, GetCustomerResponse>();
            var response = await client.Request(new GetCustomerRequest(Guid.NewGuid(), customerId));
            return response;
        }

        public async Task<GetAllCustomersResponse> GetAllCustomers()
        {
            var client = _requestClientCreator.Create<IGetAllCustomersRequest, GetAllCustomersResponse>();
            var response = await client.Request(new GetAllCustomersRequest());
            return response;
        }
    }
}