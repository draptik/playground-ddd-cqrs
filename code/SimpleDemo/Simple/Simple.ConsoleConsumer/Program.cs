using System;
using MassTransit;

namespace Simple.ConsoleConsumer
{
    /// <summary>
    ///     This is am independent dummy consumer.
    /// </summary>
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

                // "simple_customer_service_console_queue" is a *new* queue/exchange for this application
                cfg.ReceiveEndpoint(host, "simple_customer_service_console_queue", ec =>
                {
                    // "simple_customer_service_queue" is the queue/exchange name which we want to listen to
                    ec.Bind("simple_customer_service_queue");

                    ec.Consumer<DummyConsumer>();
                });
            });

            busControl.Start();

            Console.ReadLine();
        }
    }
}