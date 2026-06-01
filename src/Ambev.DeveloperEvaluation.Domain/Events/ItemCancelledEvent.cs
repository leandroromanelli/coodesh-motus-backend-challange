using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record ItemCancelledEvent(Sale sale, SaleItem? Item) : IDomainEvent;