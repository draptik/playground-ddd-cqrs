using System.Threading.Tasks;
using Simple.CommandStack.Responses;
using Simple.Domain;

namespace Simple.Contracts
{
    public interface ICustomerService
    {
        Task<CreateCustomerResponse> CreateCustomer(Customer customer);
    }
}