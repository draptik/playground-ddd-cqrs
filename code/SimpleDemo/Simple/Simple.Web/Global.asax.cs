using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Simple.Infrastructure;

namespace Simple.Web
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