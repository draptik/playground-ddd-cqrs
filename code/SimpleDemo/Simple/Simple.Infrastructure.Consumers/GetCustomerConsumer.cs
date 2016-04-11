using MassTransit;
using Simple.CommandStack.Requests;
using Simple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Infrastructure.Consumers
{
    public class GetCustomerConsumer : IConsumer<GetCustomerRequest>
    {
        private readonly ICustomerRepository _repository;
        public GetCustomerConsumer(ICustomerRepository repository)
        {
            _repository = repository;

        }
        public async Task Consume(ConsumeContext<GetCustomerRequest> context)
        {
            await Task.Run(() =>
             {
                 var customer = _repository.FindById(context.Message.CustomerId);
                 return customer;
             });
            
        }
    }
}
