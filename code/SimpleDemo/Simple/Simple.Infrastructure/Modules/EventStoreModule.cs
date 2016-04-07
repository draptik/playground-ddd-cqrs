using Autofac;
using Simple.Common;
using Simple.Eventstore;

namespace Simple.Infrastructure.Modules
{
    public class EventStoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof (EventStore).Assembly).AsImplementedInterfaces();
        }
    }
}