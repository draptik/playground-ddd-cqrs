using System.Threading.Tasks;
using System.Web.Http;
using Simple.CommandStack.ViewModels;
using Simple.Contracts;
using Simple.Domain;
using System;

namespace Simple.Web.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomer(Customer customer)
        {
            var result = await _service.CreateCustomer(customer);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IHttpActionResult> ChangeAddress(ChangeCustomerAddressViewModel customer)
        {
            var result = await _service.ChangeCustomerAddress(customer.Id, customer.Address);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCustomer([FromUri] Guid customerId)
        {
            var result = await _service.GetCustomer(customerId);
            return Ok(result);
        }
    }
}