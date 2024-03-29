﻿using OrderService.Commands;
using CoreLayer.Entities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    public class OrderConsumerController : IConsumer<Product>
    {
        private readonly IMediator _mediator;
        public OrderConsumerController(IMediator mediator)
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
            await _mediator.Send(new GetOrdersQuery(order));
        }
    }
}