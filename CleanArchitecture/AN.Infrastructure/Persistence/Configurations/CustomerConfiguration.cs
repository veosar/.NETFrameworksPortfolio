using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AN.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            customerId => customerId.Value,
            value => CustomerId.Create(value));

        builder.Property(c => c.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value))
            .HasMaxLength(255);

        builder.Property(c => c.FirstName)
            .HasConversion(
                firstName => firstName.Value,
                value => FirstName.Create(value))
            .HasMaxLength(50);

        builder.Property(c => c.LastName)
            .HasConversion(
                lastName => lastName.Value,
                value => LastName.Create(value))
            .HasMaxLength(50);

        builder.HasIndex(c => c.Email).IsUnique();
    }
}