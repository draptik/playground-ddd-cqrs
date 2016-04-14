using System.Collections.Generic;
using Simple.Domain.QueryModels;

namespace Simple.Messages
{
    public interface IGetAllCustomersRequest
    {
        IEnumerable<CustomerForList> Customers { get; set; }
    }
}