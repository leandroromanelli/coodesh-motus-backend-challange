using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.Product, p =>
        {
            p.Property(pr => pr.Id).HasColumnName("ProductId");
            p.Property(pr => pr.Name).HasColumnName("ProductName").HasMaxLength(100);
        });
        builder.Property(x => x.Quantity);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2);
        builder.Property(x => x.Discount).HasPrecision(18, 2);
        builder.Property(x => x.TotalAmount).HasPrecision(18, 2);
    }
}