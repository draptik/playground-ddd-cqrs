using System;
using System.Configuration;
using Autofac;
using MassTransit;
using Simple.Infrastructure.Consumers.EventStoreHandlers;

namespace Simple.Infrastructure.Modules
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterConsumers(typeof(CreateCustomerConsumer).Assembly);

            var useInMemoryBus = bool.Parse(ConfigurationManager.AppSettings["UseInMemoryBus"]);

            if (useInMemoryBus) {
                builder.Register(ConfigureInMemoryBus)
                    .SingleInstance()
                    .As<IBusControl>()
                    .As<IBus>();
            }
            else {
                builder.Register(ConfigureRabbitMq)
                    .SingleInstance()
                    .As<IBusControl>()
                    .As<IBus>();
            }
        }

        private static IBusControl ConfigureInMemoryBus(IComponentContext context)
        {
            var busControl =
                Bus.Factory.CreateUsingInMemory(
                    cfg => { cfg.ReceiveEndpoint(ConfigurationManager.AppSettings["Endpoint"], ep => ep.LoadFrom(context)); });
            return busControl;
        }

        private static IBusControl ConfigureRabbitMq(IComponentContext context)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(ConfigurationManager.AppSettings["RabbitMqUri"]),
                    h =>
                    {
                        h.Username(
                            ConfigurationManager.AppSettings[
                                "RabbitMqUser"
                                ]);
                        h.Password(
                            ConfigurationManager.AppSettings[
                                "RabbitMqPassword"
                                ]);
                    });

                cfg.ReceiveEndpoint(host, ConfigurationManager.AppSettings["Endpoint"], ec => { ec.LoadFrom(context); });
            });
            return busControl;
        }
    }
}