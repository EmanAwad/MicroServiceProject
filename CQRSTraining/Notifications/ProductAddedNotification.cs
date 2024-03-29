﻿using CoreLayer.Entities;
using MediatR;

namespace ProductService.Notifications
{
    public record ProductAddedNotification(Product Product) : INotification;
}
