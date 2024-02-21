
using CoreLayer.Entities;
using MediatR;

namespace ProductService.Commands
{
    public record AddProductCommand(Product Product) : IRequest<Product>;
}
