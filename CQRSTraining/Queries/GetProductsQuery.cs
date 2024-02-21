using CrossCuttingLayer.Entities;
using MediatR;

namespace ProductService.Queries
{
    public record GetProductsQuery() : IRequest<IEnumerable<Product>>;
}
