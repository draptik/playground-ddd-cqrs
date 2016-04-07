using System;
using System.Configuration;
using Autofac;
using MassTransit;
using Simple.Infrastructure.Consumers;

namespace Simple.Infrastructure.Modules
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateCustomerConsumer>();

            // RabbitMq implementation
            builder.Register(context =>
            {
                var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri(ConfigurationManager.AppSettings["RabbitMqUri"]), h =>
                    {
                        h.Username(ConfigurationManager.AppSettings["RabbitMqUser"]);
                        h.Password(ConfigurationManager.AppSettings["RabbitMqPassword"]);
                    });

                    cfg.ReceiveEndpoint(host, ConfigurationManager.AppSettings["RabbitMqEndpoint"], ec => { ec.LoadFrom(context); });
                });
                return busControl;
            })
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();

            // In Memory implementation
            //builder.Register(context =>
            //{
            //    var busControl = Bus.Factory.CreateUsingInMemory(cfg =>
            //    {
            //        cfg.ReceiveEndpoint("my_queue", ep => ep.LoadFrom(context) );
            //    });
            //    return busControl;
            //}).SingleInstance()
            //    .As<IBusControl>()
            //    .As<IBus>();
        }
    }
}