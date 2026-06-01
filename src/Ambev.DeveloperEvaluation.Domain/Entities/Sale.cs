using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    private List<SaleItem> _items = new();
    private List<IDomainEvent> _events = new();

    public Guid Id { get; private set; }
    public string SaleNumber { get; private set; }
    public DateTime SaleDate { get; private set; }
    public ExternalIdentity Customer { get; private set; }
    public ExternalIdentity Branch { get; private set; }
    public IReadOnlyList<SaleItem> Items => _items;
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }
    public IReadOnlyList<IDomainEvent> Events => _events;

    private Sale() { } // EF

    public Sale(string saleNumber, DateTime saleDate, ExternalIdentity customer, ExternalIdentity branch)
    {
        Id = Guid.NewGuid();
        SaleNumber = saleNumber;
        SaleDate = saleDate;
        Customer = customer;
        Branch = branch;
        IsCancelled = false;
        AddEvent(new SaleCreatedEvent(this));
    }

    public void AddItem(ExternalIdentity product, int quantity, decimal unitPrice)
    {
        if (IsCancelled) throw new Exception("Cannot add item to cancelled sale.");
        var item = new SaleItem(product, quantity, unitPrice);
        _items.Add(item);
        RecalculateTotal();
        AddEvent(new SaleModifiedEvent(this));
    }

    public void UpdateItemQuantity(Guid itemId, int newQuantity)
    {
        if (IsCancelled) throw new Exception("Cannot update cancelled sale.");
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null) throw new Exception("Item not found.");
        item.UpdateQuantity(newQuantity);
        RecalculateTotal();
        AddEvent(new SaleModifiedEvent(this));
    }

    public void CancelSale()
    {
        if (IsCancelled) return;
        IsCancelled = true;
        AddEvent(new SaleCancelledEvent(this));
    }

    public void CancelItem(Guid itemId)
    {
        if (IsCancelled) throw new Exception("Cannot cancel item from cancelled sale.");
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null) throw new Exception("Item not found.");
        item.Cancel();
        RecalculateTotal();
        AddEvent(new ItemCancelledEvent(this, item));
        AddEvent(new SaleModifiedEvent(this));
    }

    private void RecalculateTotal()
    {
        TotalAmount = _items.Sum(i => i.TotalAmount);
    }

    private void AddEvent(IDomainEvent e) => _events.Add(e);
    public void ClearEvents() => _events.Clear();
}