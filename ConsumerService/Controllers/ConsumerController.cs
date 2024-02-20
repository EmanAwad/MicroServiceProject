using ConsumerService.Commands;
using CrossCuttingLayer.Core.Entities;
using CrossCuttingLayer.Entities;
using MassTransit;
using MediatR;

namespace ConsumerService.Controllers
{
    public class ConsumerController : IConsumer<Product>
    {
        private readonly IMediator _mediator;
        public ConsumerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<Product> context)
        {
            var data = context.Message;
            Order order = new Order
            {
                Id=1,
                ProductId = data.Id
            };
            await _mediator.Send(new AddOrderCommand(order));
        }
    }
}


