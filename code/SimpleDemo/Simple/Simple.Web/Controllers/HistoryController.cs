using System;
using System.Threading.Tasks;
using System.Web.Http;
using Simple.Contracts;

namespace Simple.Web.Controllers
{
    public class HistoryController : ApiController
    {
        private readonly IHistoryService _service;

        public HistoryController(IHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetHistoryForCustomer([FromUri] Guid customerId)
        {
            var result = await _service.GetHistoryForCustomer(customerId);
            return Ok(result);
        }
    }
}