using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public Guid Id { get; private set; }
    public ExternalIdentity Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }

    private SaleItem() { }

    public SaleItem(ExternalIdentity product, int quantity, decimal unitPrice)
    {
        Id = Guid.NewGuid();
        Product = product;
        UnitPrice = unitPrice;
        SetQuantity(quantity);
        ApplyDiscount();
    }

    private void SetQuantity(int quantity)
    {
        if (quantity <= 0) throw new Exception("Quantity must be > 0");
        if (quantity > 20) throw new Exception("Cannot sell more than 20 identical items");
        Quantity = quantity;
    }

    private void ApplyDiscount()
    {
        if (IsCancelled)
        {
            Discount = 0;
            TotalAmount = 0;
            return;
        }
        if (Quantity >= 10) Discount = 0.20m;
        else if (Quantity >= 4) Discount = 0.10m;
        else Discount = 0;

        TotalAmount = Quantity * UnitPrice * (1 - Discount);
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (IsCancelled) throw new Exception("Cannot update cancelled item");
        SetQuantity(newQuantity);
        ApplyDiscount();
    }

    public void Cancel()
    {
        if (IsCancelled) return;
        IsCancelled = true;
        ApplyDiscount();
    }
}