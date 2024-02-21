using ProductService.Notifications;
using CrossCuttingLayer.Entities;
using MediatR;

namespace ProductService.Handlers
{
    public class CacheInvalidationHandler : INotificationHandler<ProductAddedNotification>
    {
        private readonly DataStore _dataStore;
        public CacheInvalidationHandler(DataStore dataStore) => _dataStore = dataStore;
        public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
        {
            await _dataStore.EventOccured(notification.Product, "Cache Invalidated");
            await Task.CompletedTask;
        }
    }
}
