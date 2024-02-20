using ConsumerService.Queries;
using CrossCuttingLayer.Entities;
using MediatR;

namespace ConsumerService.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Order>>
    {
        private readonly DataStore _dataStore;

        public GetOrdersHandler(DataStore dataStore) => _dataStore = dataStore;

        public async Task<IEnumerable<Order>> Handle(GetOrdersQuery request,
            CancellationToken cancellationToken) => await _dataStore.GetAllOrders();
    }
}
