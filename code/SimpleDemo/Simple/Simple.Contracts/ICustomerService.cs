using System;
using System.Threading.Tasks;
using Simple.CommandStack.Responses;
using Simple.CommandStack.ViewModels;
using Simple.Domain;

namespace Simple.Contracts
{
    public interface ICustomerService
    {
        Task<CreateCustomerResponse> CreateCustomer(CreateCustomerViewModel customer);
        Task<ChangeCustomerAddressResponse> ChangeCustomerAddress(Guid customerId, string address);

        Task<GetCustomerResponse> GetCustomer(Guid customerId);
        Task<GetAllCustomersResponse> GetAllCustomers();
    }
}