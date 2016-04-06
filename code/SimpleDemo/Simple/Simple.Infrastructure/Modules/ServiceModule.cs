using Autofac;
using Simple.ApplicationService;

namespace Simple.Infrastructure.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof (CustomerService).Assembly).AsImplementedInterfaces();
        }
    }
}