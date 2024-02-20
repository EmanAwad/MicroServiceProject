using CrossCuttingLayer.Entities;
using MediatR;

namespace ConsumerService.Queries
{
    public record GetOrdersQuery() : IRequest<IEnumerable<Order>>;
}
