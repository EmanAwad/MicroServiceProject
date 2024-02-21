using ProductService.Queries;
using CoreLayer.Entities;
using MediatR;

namespace ProductService.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly DataStore _dataStore;

        public GetProductByIdHandler(DataStore dataStore) => _dataStore = dataStore;

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
            await _dataStore.GetProductById(request.Id);

    }
}
