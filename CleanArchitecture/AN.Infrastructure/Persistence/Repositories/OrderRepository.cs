using AN.Domain.Entities.Orders;
using AN.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AN.Infrastructure.Persistence.Repositories;

internal sealed class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }

    public bool HasOneLineItem(Order order)
    {
        return _context.LineItems.Count(li => li.OrderId == order.Id) == 1;
    }

    public async Task<Order?> GetByIdAsync(OrderId id)
    {
        return await _context.Orders
            .Include(o => o.LineItems)
            .SingleOrDefaultAsync(o => o.Id == id);
    }

    public void Update(Order order)
    {
        // This is not necessary as EF will automatically track changes
        // _context.Orders.Update(order);
    }
}