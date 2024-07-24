using AN.Domain.Entities.Orders;

namespace AN.Domain.Interfaces.Repositories;

public interface IOrderSummaryRepository
{
    void Add(OrderSummary orderSummary);
    Task<OrderSummary?> GetByIdAsync(OrderId orderId);
}