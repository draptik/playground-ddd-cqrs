using System;
using System.Threading.Tasks;
using Simple.CommandStack.Responses;

namespace Simple.Contracts
{
    public interface IHistoryService
    {
        Task<GetHistoryForCustomerResponse> GetHistoryForCustomer(Guid customerId);
    }
}