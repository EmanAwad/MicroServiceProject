
using CrossCuttingLayer.Entities;
using MediatR;

namespace CQRSTraining.Commands
{
    public record AddProductCommand(Product Product) : IRequest<Product>;
}
