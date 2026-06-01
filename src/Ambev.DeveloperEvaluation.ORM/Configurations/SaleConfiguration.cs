using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SaleNumber).HasMaxLength(50).IsRequired();
        builder.HasIndex(x => x.SaleNumber).IsUnique();
        builder.OwnsOne(x => x.Customer, c =>
        {
            c.Property(p => p.Id).HasColumnName("CustomerId");
            c.Property(p => p.Name).HasColumnName("CustomerName").HasMaxLength(100);
        });
        builder.OwnsOne(x => x.Branch, b =>
        {
            b.Property(p => p.Id).HasColumnName("BranchId");
            b.Property(p => p.Name).HasColumnName("BranchName").HasMaxLength(100);
        });
        builder.HasMany(x => x.Items).WithOne().HasForeignKey("SaleId").OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.TotalAmount).HasPrecision(18, 2);
        builder.Ignore(x => x.Events);
    }
}