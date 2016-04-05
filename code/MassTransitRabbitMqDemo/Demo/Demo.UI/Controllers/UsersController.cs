using System.Threading.Tasks;
using System.Web.Http;
using Demo.Contracts;
using Demo.UI.Application;

namespace Demo.UI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly UserService service;

        public UsersController()
        {
            this.service = new UserService();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            CreateNewUserResponse response = await this.service.CreateNewUser("foo");
            return this.Ok(response.Message);
        }

    }
}