using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Orders;
using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AN.Application.Data;

public interface IApplicationDbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderSummary> OrderSummaries { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}