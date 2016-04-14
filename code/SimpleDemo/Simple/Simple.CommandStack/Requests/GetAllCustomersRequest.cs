using System.Collections.Generic;
using Simple.Domain.QueryModels;

namespace Simple.CommandStack.Requests
{
    public class GetAllCustomersRequest : IGetAllCustomersRequest
    {
        public IEnumerable<CustomerForList> Customers { get; set; }
    }
}