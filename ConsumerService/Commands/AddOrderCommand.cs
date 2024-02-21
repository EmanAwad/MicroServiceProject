
using CoreLayer.Entities;
using MediatR;

namespace OrderService.Commands
{
    public record GetOrdersQuery(Order Order) : IRequest<Order>;
}
