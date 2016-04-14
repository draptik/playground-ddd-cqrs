using System;
using Simple.Domain;
using Simple.Domain.QueryModels;

namespace Simple.CommandStack.Responses
{
    public class GetCustomerResponse
    {
        public Guid ResponseId { get; set; }
        public string Message { get; set; }
        public CustomerDetails Customer { get; set; }
    }
}