
using ConsumerService.Commands;
using CrossCuttingLayer.Core.Entities;
using CrossCuttingLayer.Entities;
using MassTransit;
using MediatR;

namespace ConsumerService.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrderCommand, Order>
    {
        private readonly DataStore _dataStore;
        private readonly IPublishEndpoint _publishEndpoint;
        public AddOrderHandler(DataStore dataStore, IPublishEndpoint publishEndpoint)
        {
            _dataStore = dataStore;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            await _dataStore.AddOrder(request.Order);
            return request.Order;
        }
    }
}
