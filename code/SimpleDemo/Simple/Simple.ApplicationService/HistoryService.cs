using System;
using System.Configuration;
using System.Threading.Tasks;
using MassTransit;
using Simple.CommandStack.Requests;
using Simple.CommandStack.Responses;
using Simple.Contracts;
using Simple.Messages;

namespace Simple.ApplicationService
{
    public class HistoryService : IHistoryService
    {
        private readonly IBusControl _bus;
        private readonly Uri _serviceAddress;

        public HistoryService(IBusControl bus)
        {
            _bus = bus;

            var useInMemoryBus = bool.Parse(ConfigurationManager.AppSettings["UseInMemoryBus"]);
            _serviceAddress = useInMemoryBus
                ? new Uri(ConfigurationManager.AppSettings["ServiceAddressInMemory"])
                : new Uri(ConfigurationManager.AppSettings["ServiceAddress"]);
        }

        public async Task<GetHistoryForCustomerResponse> GetHistoryForCustomer(Guid customerId)
        {
            var client = CreateHistoryClient();
            var response = await client.Request(new GetHistoryForCustomerRequest {RequestId = Guid.NewGuid(), CustomerId = customerId});
            return response;
        }

        private IRequestClient<IGetHistoryForCustomerRequest, GetHistoryForCustomerResponse> CreateHistoryClient()
        {
            return
                _bus.CreateRequestClient<IGetHistoryForCustomerRequest, GetHistoryForCustomerResponse>(
                    this._serviceAddress, TimeSpan.FromSeconds(30));
        }
    }
}