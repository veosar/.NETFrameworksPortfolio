using AN.Application.Data;
using AN.Domain.Entities.Orders;
using AN.Domain.Exceptions.Entities.Orders;
using AN.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AN.Infrastructure.Persistence.Repositories;

public class OrderSummariesRepository : IOrderSummaryRepository
{
    private readonly IApplicationDbContext _context;

    public OrderSummariesRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(OrderSummary orderSummary)
    {
        _context.OrderSummaries.Add(orderSummary);
    }

    public async Task<OrderSummary?> GetByIdAsync(OrderId orderId)
    {
        var orderSummary = await _context.OrderSummaries
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId.Value);

        return orderSummary;
    }
}