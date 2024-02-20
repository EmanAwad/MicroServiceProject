using CrossCuttingLayer.Entities;
using MediatR;

namespace CQRSTraining.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Product>;
}
