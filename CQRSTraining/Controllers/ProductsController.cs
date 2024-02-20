using CQRSTraining.Commands;
using CQRSTraining.Notifications;
using CQRSTraining.Queries;
using CrossCuttingLayer.Core;
using CrossCuttingLayer.Entities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CQRSTraining.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBus _publishEndpoint;
        public ProductsController(IMediator mediator, IBus publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;

        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetProductsQuery());

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }
        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            {
                Uri uri = new Uri(RabbitMqConsts.RabbitMqUri);
                var endPoint = await _publishEndpoint.GetSendEndpoint(uri);
                await endPoint.Send(product);
                var productToReturn = await _mediator.Send(new AddProductCommand(product));
                await _mediator.Publish(new ProductAddedNotification(productToReturn));
                return Ok(productToReturn.Id);
            }
        }
    }
}
