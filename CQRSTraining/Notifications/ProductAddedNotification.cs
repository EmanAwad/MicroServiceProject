using CrossCuttingLayer.Entities;
using MediatR;

namespace CQRSTraining.Notifications
{
    public record ProductAddedNotification(Product Product) : INotification;
}
