
using CrossCuttingLayer.Entities;
using MediatR;

namespace ConsumerService.Commands
{
    public record GetOrdersQuery(Order Order) : IRequest<Order>;
}
