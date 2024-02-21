using ProductService.Commands;
using CrossCuttingLayer.Entities;
using MassTransit;
using MediatR;

namespace ProductService.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly DataStore _dataStore;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBus _bus;
        public AddProductHandler(DataStore dataStore, IPublishEndpoint publishEndpoint,IBus bus)
        {
            _dataStore = dataStore;
            _publishEndpoint = publishEndpoint;
            _bus = bus;
        }
        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _dataStore.AddProduct(request.Product);
            await _publishEndpoint.Publish<AddProductCommand>(new
            {
                request.Product.Id,
                request.Product.Name,
            }, cancellationToken);
            //await _bus.Send(request.Product,cancellationToken);
            return request.Product;
        }
    }
}
