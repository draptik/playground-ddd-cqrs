using System;
using MassTransit;

namespace Simple.ConsoleConsumer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/simple"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "simple_customer_service_console_queue", ec =>
                {
                    ec.Bind("simple_customer_service_queue");
                    ec.Consumer<DummyConsumer>();
                });
            });

            busControl.Start();
            
            Console.ReadLine();
        }
    }
}