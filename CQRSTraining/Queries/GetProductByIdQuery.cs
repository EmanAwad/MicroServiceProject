using CrossCuttingLayer.Entities;
using MediatR;

namespace ProductService.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Product>;
}
