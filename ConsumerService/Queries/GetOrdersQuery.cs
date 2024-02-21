using CrossCuttingLayer.Entities;
using MediatR;

namespace StockService.Queries
{
    public record GetOrdersQuery() : IRequest<IEnumerable<Order>>;
}
