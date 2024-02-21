using CoreLayer.Entities;
using MediatR;

namespace OrderService.Queries
{
    public record GetOrdersQuery() : IRequest<IEnumerable<Order>>;
}
