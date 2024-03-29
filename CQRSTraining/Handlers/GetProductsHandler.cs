﻿
using ProductService.Queries;
using CoreLayer.Entities;
using MediatR;

namespace ProductService.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly DataStore _dataStore;

        public GetProductsHandler(DataStore dataStore) => _dataStore = dataStore;

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request,
            CancellationToken cancellationToken) => await _dataStore.GetAllProducts();
    }
}
