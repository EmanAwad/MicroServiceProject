using CQRSTraining.Commands;
using CrossCuttingLayer.Entities;
using MassTransit;
using MediatR;

namespace CQRSTraining.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly DataStore _dataStore;
        private readonly IPublishEndpoint _publishEndpoint;
        public AddProductHandler(DataStore dataStore, IPublishEndpoint publishEndpoint)
        {
            _dataStore = dataStore;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _dataStore.AddProduct(request.Product);
            await _publishEndpoint.Publish<AddProductCommand>(new
            {
                request.Product.Id,
                request.Product.Name,
            }, cancellationToken);
            return request.Product;
        }
    }
}
