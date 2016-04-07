using Autofac;
using Simple.Common;

namespace Simple.Infrastructure.Modules
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof (EventStore).Assembly).AsImplementedInterfaces();
        }
    }
}