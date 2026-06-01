using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain
{
    public class SaleTests
    {
        [Fact]
        public void AddItem_Quantity10_ShouldApply20PercentDiscount()
        {
            var sale = new Sale("S1", DateTime.UtcNow, new ExternalIdentity(Guid.NewGuid(), "C"), new ExternalIdentity(Guid.NewGuid(), "B"));
            sale.AddItem(new ExternalIdentity(Guid.NewGuid(), "P"), 10, 100m);
            var item = sale.Items.First();
            Assert.Equal(0.20m, item.Discount);
            Assert.Equal(800m, item.TotalAmount);
        }
    }
}
