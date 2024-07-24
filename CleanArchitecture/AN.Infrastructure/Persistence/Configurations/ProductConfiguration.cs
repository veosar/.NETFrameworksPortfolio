using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AN.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(
            productId => productId.Value,
            value => ProductId.Create(value));

        builder.Property(p => p.Name)
            .HasConversion(
                name => name.Value,
                value => ProductName.Create(value))
            .HasMaxLength(255);

        builder.Property(p => p.Sku)
            .HasConversion(
                sku => sku.Value,
                value => Sku.Create(value))
            .HasMaxLength(15);

        builder.OwnsOne(p => p.Price, pb => {
            pb.Property(m => m.Amount).HasColumnName("Price");
            pb.Property(m => m.CurrencyId)
                .HasConversion(
                    currencyId => currencyId.Value,
                    value => CurrencyId.Create(value));
        });
    }
}