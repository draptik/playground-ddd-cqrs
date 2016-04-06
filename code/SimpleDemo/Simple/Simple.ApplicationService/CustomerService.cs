using System.Threading.Tasks;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Domain;

namespace Simple.ApplicationService
{
    public class CustomerService : ICustomerService
    {
        public Task<CreateCustomerResponse> CreateCustomer(Customer customer)
        {
            
            
            // TODO Create request and send to consumer via bus client



            throw new System.NotImplementedException();
        }
    }
}