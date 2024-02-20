
using CrossCuttingLayer.Core.Entities;
using CrossCuttingLayer.Entities;
using MediatR;

namespace ConsumerService.Commands
{
    public record AddOrderCommand(Order Order) : IRequest<Order>;
}
