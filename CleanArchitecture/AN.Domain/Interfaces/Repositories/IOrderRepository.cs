using AN.Domain.Entities.Orders;

namespace AN.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
    bool HasOneLineItem(Order order);
    Task<Order?> GetByIdAsync(OrderId id);
    void Update(Order order);
}