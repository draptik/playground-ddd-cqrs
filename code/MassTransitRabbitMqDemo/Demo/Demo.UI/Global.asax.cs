using System;
using System.Web;
using System.Web.Http;
using MassTransit;

namespace Demo.UI
{
    public class MyApp : HttpApplication
    {
        private static IBusControl _bus;

        public static IBusControl MyBus => _bus;

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            _bus = ConfigureBus();
            _bus.Start();
        }

        private IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/test"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }
    }
}