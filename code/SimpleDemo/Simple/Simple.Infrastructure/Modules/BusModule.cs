using Autofac;
using MassTransit;

namespace Simple.Infrastructure.Modules
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // TODO register consumers

            builder.Register(context =>
            {
                var busControl = Bus.Factory.CreateUsingInMemory(cfg =>
                {
                    cfg.ReceiveEndpoint("my-queue", ep => ep.LoadFrom(context) );
                });
                return busControl;
            }).SingleInstance()
                .As<IBusControl>()
                .As<IBus>();
        }
    }
}