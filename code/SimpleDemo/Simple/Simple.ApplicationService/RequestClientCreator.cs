using System;
using System.Configuration;
using MassTransit;

namespace Simple.ApplicationService
{
    public class RequestClientCreator : IRequestClientCreator
    {
        private readonly IBusControl bus;
        private readonly Uri serviceAddress;

        public RequestClientCreator(IBusControl bus)
        {
            this.bus = bus;
            var useInMemoryBus = bool.Parse(ConfigurationManager.AppSettings["UseInMemoryBus"]);
            this.serviceAddress = useInMemoryBus
                ? new Uri(ConfigurationManager.AppSettings["ServiceAddressInMemory"])
                : new Uri(ConfigurationManager.AppSettings["ServiceAddress"]);
        }

        public IRequestClient<TRequest, TResponse> Create<TRequest, TResponse>()
            where TRequest : class
            where TResponse : class
        {
            return this.bus.CreateRequestClient<TRequest, TResponse>(this.serviceAddress, TimeSpan.FromSeconds(30));
        }
    }
}