using System.Collections;
using System.Collections.Generic;
using Simple.Domain.QueryModels;

namespace Simple.CommandStack.Responses
{
    public class GetAllCustomersResponse
    {
        public IEnumerable<CustomerForList> Customers { get; set; }
        public string Message { get; set; }

    }
}