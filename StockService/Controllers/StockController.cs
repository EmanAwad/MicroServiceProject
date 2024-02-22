using CoreLayer.Core.Entities;
using CoreLayer.Entities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockService.Command;

namespace StockService.Controllers
{
    public class StockController : IConsumer<Stock>
    {
        private readonly IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<Stock> context)
        {
            var data = context.Message;
            Stock stock = new Stock
            {
                Id=data.Id,
                ProductId = data.ProductId
            };
            await _mediator.Send(new StockEventOccuredCommand(stock));
        }
    }
}