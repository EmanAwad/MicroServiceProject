using CrossCuttingLayer.Entities;
using MediatR;

namespace CQRSTraining.Queries
{
    public record GetProductsQuery() : IRequest<IEnumerable<Product>>;
}
