using CoreLayer.Core.Entities;
using CoreLayer.Entities;
using MassTransit;
using MediatR;
using StockService.Command;

namespace StockService.Handler
{
    public class StockEventOccuredHandler : IRequestHandler<StockEventOccuredCommand, Stock>
    {
        private readonly DataStore _dataStore;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBus _bus;
        public StockEventOccuredHandler(DataStore dataStore, IPublishEndpoint publishEndpoint, IBus bus)
        {
            _dataStore = dataStore;
            _publishEndpoint = publishEndpoint;
            _bus = bus;
        }
        public async Task<Stock> Handle(StockEventOccuredCommand request, CancellationToken cancellationToken)
        {
            await _dataStore.StockEventOccured(request.stock, "Edited");
            await _publishEndpoint.Publish<StockEventOccuredCommand>(new
            {
                request.stock.Id,
            }, cancellationToken);

            return request.stock;
        }
    }
}
