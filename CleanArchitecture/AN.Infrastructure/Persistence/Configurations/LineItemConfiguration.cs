using AN.Domain.Entities.Orders;
using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AN.Infrastructure.Persistence.Configurations;

public class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.HasKey(li => li.Id);
        builder.Property(li => li.Id).HasConversion(
            lineItemId => lineItemId.Value,
            value => LineItemId.Create(value));

        builder.Property(li => li.OrderId).HasConversion(
            orderId => orderId.Value,
            value => OrderId.Create(value));

        builder.Property(li => li.ProductId).HasConversion(
            productId => productId.Value,
            value => ProductId.Create(value));

        builder.OwnsOne(li => li.Price, pb => {
            pb.Property(p => p.Amount).HasColumnName("Price");
            pb.Property(p => p.CurrencyId).HasConversion(
                currencyId => currencyId.Value,
                value => CurrencyId.Create(value));
        });
    }
}