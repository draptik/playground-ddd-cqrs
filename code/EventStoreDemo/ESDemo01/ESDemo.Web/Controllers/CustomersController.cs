using System.Threading.Tasks;
using System.Web.Http;
using ESDEmo.Domain;

namespace ESDemo.Web.Controllers
{
    public class CustomersController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateCustomer newCustomer)
        {
            return null;
        }
    }
}