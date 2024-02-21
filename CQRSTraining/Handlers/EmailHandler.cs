using ProductService.Notifications;
using CoreLayer.Entities;
using MediatR;

namespace ProductService.Handlers
{
    public class EmailHandler : INotificationHandler<ProductAddedNotification>
    {
        private readonly DataStore _dataStore;
        public EmailHandler(DataStore dataStore) => _dataStore = dataStore;
        public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
        {
            await _dataStore.EventOccured(notification.Product, "Email Sent");
            await Task.CompletedTask;
        }
    }
}
