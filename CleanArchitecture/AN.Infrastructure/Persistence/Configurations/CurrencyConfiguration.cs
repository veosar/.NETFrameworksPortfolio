using AN.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AN.Infrastructure.Persistence.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .HasConversion(
                currencyId => currencyId.Value,
                value => CurrencyId.Create(value));

        builder.Property(c => c.Name)
            .HasConversion(
                name => name.Value,
                value => CurrencyName.Create(value))
            .HasMaxLength(3);

        builder.HasIndex(c => c.Name).IsUnique();
    }
}