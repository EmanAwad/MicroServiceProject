
using OrderService.Commands;
using CoreLayer.Entities;
using MassTransit;
using MediatR;

namespace OrderService.Handlers
{
    public class AddOrderHandler : IRequestHandler<GetOrdersQuery, Order>
    {
        private readonly DataStore _dataStore;
        private readonly IPublishEndpoint _publishEndpoint;
        public AddOrderHandler(DataStore dataStore, IPublishEndpoint publishEndpoint)
        {
            _dataStore = dataStore;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Order> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            await _dataStore.AddOrder(request.Order);
            return request.Order;
        }
    }
}
