using BogusCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace BogusDBApi;

public class OrdersDbContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }
}