
using CrossCuttingLayer.Entities;
using MediatR;

namespace StockService.Commands
{
    public record GetOrdersQuery(Order Order) : IRequest<Order>;
}
