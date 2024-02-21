using CQRSTraining.Commands;
using CQRSTraining.Notifications;
using CQRSTraining.Queries;
using CQRSTraining.RedisCashe;
using CrossCuttingLayer.Core;
using CrossCuttingLayer.Entities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;

namespace CQRSTraining.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBus _publishEndpoint;
        private readonly IMemoryCache _memoryCache;
        private readonly ICacheService _cacheService;
        public ProductsController(IMediator mediator, IBus publishEndpoint, IMemoryCache memoryCache, ICacheService cacheService)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _memoryCache = memoryCache;
            _cacheService = cacheService;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            //var products = await _mediator.Send(new GetProductsQuery());
            //return Ok(products);
            var cacheData = _cacheService.GetData<IEnumerable<Product>>("product");
            if (cacheData != null)
            {
                return cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            cacheData = await _mediator.Send(new GetProductsQuery());
            _cacheService.SetData<IEnumerable<Product>>("product", cacheData, expirationTime);
            return cacheData;
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<Product> GetProductById(int id)
        {
            //var product = await _mediator.Send(new GetProductByIdQuery(id));
            //return Ok(product);
            Product filteredData;
            var cacheData = _cacheService.GetData<IEnumerable<Product>>("product");
            if (cacheData != null)
            {
                filteredData = cacheData.Where(x => x.Id == id).FirstOrDefault();
                return filteredData;
            }
            filteredData = await _mediator.Send(new GetProductByIdQuery(id));
            return filteredData;
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
                _cacheService.RemoveData("product");
                await _mediator.Publish(new ProductAddedNotification(productToReturn));
                return Ok(productToReturn.Id);
            }
        }
        [HttpGet]
        [Route("GetProductFromMemoryCache")]
        public async Task<ActionResult> GetProductFromMemoryCache()
        {
            var cacheData = _memoryCache.Get<IEnumerable<Product>>("products");
            if (cacheData != null)
            {
                return Ok(cacheData);
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            cacheData = await _mediator.Send(new GetProductsQuery());
            _memoryCache.Set("products", cacheData, expirationTime);
            return Ok(cacheData);
        }
      
    }
}
