﻿using CoreLayer.Entities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StockService.Controllers
{
    public class StockController : IConsumer<Product>
    {
        private readonly IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<Product> context)
        {
            //var data = context.Message;
            //Order order = new Order
            //{
            //    Id=1,
            //    ProductId = data.Id
            //};
            //await _mediator.Send(new GetOrdersQuery(order));
        }
    }
}