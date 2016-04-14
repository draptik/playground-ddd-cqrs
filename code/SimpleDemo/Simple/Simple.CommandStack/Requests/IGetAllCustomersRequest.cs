using System.Collections;
using System.Collections.Generic;
using Simple.Domain.QueryModels;

namespace Simple.CommandStack.Requests
{
    public interface IGetAllCustomersRequest
    {
        IEnumerable<CustomerForList> Customers { get; set; }
    }
}