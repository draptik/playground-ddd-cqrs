using System;
using Simple.Domain;

namespace Simple.CommandStack.Responses
{
    public class GetCustomerResponse
    {
        public Guid ResponseId { get; set; }
        public string Message { get; set; }
        public Customer Customer { get; set; }
    }
}