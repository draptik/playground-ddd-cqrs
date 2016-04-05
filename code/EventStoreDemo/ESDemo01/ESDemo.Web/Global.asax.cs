using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using ESDemo.Infrastructure;

namespace ESDemo.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IoCBuilder.InitWeb(Assembly.GetExecutingAssembly(), GlobalConfiguration.Configuration);
        }
    }
}