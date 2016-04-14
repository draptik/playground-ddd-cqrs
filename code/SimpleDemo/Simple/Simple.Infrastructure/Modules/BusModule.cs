using System;
using System.Configuration;
using Autofac;
using MassTransit;
using Simple.Infrastructure.Consumers;
using Simple.Infrastructure.Consumers.EventStoreHandlers;
using Simple.Infrastructure.Consumers.ReadModelHandlers;

namespace Simple.Infrastructure.Modules
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(typeof(CreateCustomerConsumer).Assembly).AsImplementedInterfaces();

            //builder.RegisterType<CreateCustomerConsumer>();
            //builder.RegisterType<ChangeCustomerAddress>();
            //builder.RegisterType<GetCustomerConsumer>();
            //builder.RegisterType<UpdateReadModelConsumer>();
            //builder.RegisterType<UpdateCustomerCondensedReadModelConsumer>();


            builder.RegisterAssemblyTypes(typeof(CreateCustomerConsumer).Assembly)
                   .AsImplementedInterfaces()
                   .AsSelf();

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
            var busControl = Bus.Factory.CreateUsingInMemory(cfg =>
            {
                cfg.ReceiveEndpoint(ConfigurationManager.AppSettings["Endpoint"], ep => ep.LoadFrom(context));
            });
            return busControl;
        }

        private static IBusControl ConfigureRabbitMq(IComponentContext context)
        {
            ILifetimeScope scope = context.Resolve<ILifetimeScope>();


            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(ConfigurationManager.AppSettings["RabbitMqUri"]), h =>
                {
                    h.Username(ConfigurationManager.AppSettings["RabbitMqUser"]);
                    h.Password(ConfigurationManager.AppSettings["RabbitMqPassword"]);
                });

                cfg.ReceiveEndpoint(host, ConfigurationManager.AppSettings["Endpoint"], ec =>
                {

                    // This does not work when Consumer Event is fired from within another consumer:
                    //ec.LoadFrom(context);

                    // Manual registration of consumers (this can be improved...):
                    ec.Consumer<UpdateCustomerCondensedReadModelConsumer>(context.Resolve<ILifetimeScope>());
                    ec.Consumer<UpdateReadModelConsumer>(context.Resolve<ILifetimeScope>());
                    ec.Consumer<CreateCustomerConsumer>(context.Resolve<ILifetimeScope>());
                    ec.Consumer<ChangeCustomerAddress>(context.Resolve<ILifetimeScope>());
                    ec.Consumer<GetCustomerConsumer>(context.Resolve<ILifetimeScope>());
                    ec.Consumer<UpdateReadModelAfterAddressChanged>(context.Resolve<ILifetimeScope>());
                    ec.Consumer<GetAllCustomersConsumer>(context.Resolve<ILifetimeScope>());
                });
            });
            return busControl;
        }
    }
}