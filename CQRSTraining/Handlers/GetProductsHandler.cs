
using CQRSTraining.Queries;
using CrossCuttingLayer.Entities;
using MediatR;

namespace CQRSTraining.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly DataStore _dataStore;

        public GetProductsHandler(DataStore dataStore) => _dataStore = dataStore;

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request,
            CancellationToken cancellationToken) => await _dataStore.GetAllProducts();
    }
}
