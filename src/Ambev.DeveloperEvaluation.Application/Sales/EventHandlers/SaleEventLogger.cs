using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

public class SaleEventLogger :
    INotificationHandler<SaleCreatedEvent>,
    INotificationHandler<SaleModifiedEvent>,
    INotificationHandler<SaleCancelledEvent>,
    INotificationHandler<ItemCancelledEvent>
{
    private readonly ILogger<SaleEventLogger> _logger;
    public Task Handle(SaleCreatedEvent notification, CancellationToken ct)
    {
        _logger.LogInformation("Event: SaleCreated - Sale {SaleNumber}", notification.Sale.SaleNumber);
        return Task.CompletedTask;
    }

    public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event: SaleModified - Sale {SaleNumber}", notification.Sale.SaleNumber);
        return Task.CompletedTask;
    }

    public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event: SaleCancelled - Sale {SaleNumber}", notification.Sale.SaleNumber);
        return Task.CompletedTask;
    }

    public Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event: ItemCancelled - Item {SaleNumber}", notification.Item.Product);
        return Task.CompletedTask;
    }
}