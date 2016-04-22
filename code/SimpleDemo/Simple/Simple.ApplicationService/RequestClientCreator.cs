using System;
using System.Configuration;
using MassTransit;

namespace Simple.ApplicationService
{
    public class RequestClientCreator : IRequestClientCreator
    {
        private readonly IBusControl _bus;
        private readonly Uri _serviceAddress;

        public RequestClientCreator(IBusControl bus)
        {
            _bus = bus;
            var useInMemoryBus = bool.Parse(ConfigurationManager.AppSettings["UseInMemoryBus"]);
            _serviceAddress = useInMemoryBus
                ? new Uri(ConfigurationManager.AppSettings["ServiceAddressInMemory"])
                : new Uri(ConfigurationManager.AppSettings["ServiceAddress"]);
        }

        public IRequestClient<TRequest, TResponse> Create<TRequest, TResponse>()
            where TRequest : class
            where TResponse : class
        {
            return _bus.CreateRequestClient<TRequest, TResponse>(_serviceAddress, TimeSpan.FromSeconds(30));
        }
    }
}