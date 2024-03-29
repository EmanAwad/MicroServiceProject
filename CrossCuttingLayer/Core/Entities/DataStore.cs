﻿using CoreLayer.Core.Entities;
using CoreLayer.Entities;

namespace CoreLayer.Entities
{
    public class DataStore
    {
        private static List<Product> _products;
        private static List<Order> _orders;
        private static List<Stock> _stocks;

        public DataStore()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1" },
                new Product { Id = 2, Name = "Test Product 2" },
                new Product { Id = 3, Name = "Test Product 3" }
            };
            _orders = new List<Order>
            {
                new Order { Id = 1, ProductId = 1 },
                new Order { Id = 2, ProductId = 2 },
                new Order { Id = 3, ProductId = 3 }
            };
            _stocks = new List<Stock>
            {
                new Stock { Id = 1, ProductId = 1 ,NumOfProduct =5, },
                new Stock { Id = 2, ProductId = 2 ,NumOfProduct =5,},
                new Stock { Id = 3, ProductId = 3 ,NumOfProduct =5,}
            };
        }
        public async Task AddProduct(Product product)
        {
            _products.Add(product);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> GetAllProducts() => await Task.FromResult(_products);

        public async Task<Product> GetProductById(int id) =>
            await Task.FromResult(_products.Single(p => p.Id == id));

        public async Task EventOccured(Product product, string evt)
        {
            _products.Single(p => p.Id == product.Id).Name = $"{product.Name} evt: {evt}";
            await Task.CompletedTask;
        }
        public async Task<IEnumerable<Order>> GetAllOrders() => await Task.FromResult(_orders);
        public async Task AddOrder(Order order)
        {
            _orders.Add(order);
            await Task.CompletedTask;
        }
        public async Task StockEventOccured(Stock stock, string evt)
        {
            var obj = _stocks.Single(p => p.ProductId == stock.Id);
            stock.NumOfProduct = stock.NumOfProduct + 1;    
            await Task.CompletedTask;
        }
    }

}
