using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Primitives;
using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Orders;
using AN.Domain.Interfaces.Repositories;

namespace AN.Domain.Entities.Orders;

public class Order : Entity
{
    private Order()
    {
        
    }
    private readonly List<LineItem> _lineItems = [];
    public OrderId Id { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public IReadOnlyList<LineItem> LineItems => _lineItems.AsReadOnly();

    public static Order Create(CustomerId customerId)
    {
        var order = new Order
        {
            Id = OrderId.Create(Guid.NewGuid()),
            CustomerId = customerId
        };
        
        order.Raise(new OrderCreatedDomainEvent(Guid.NewGuid(), order.Id));
        return order;
    }

    public void AddLineItem(ProductId productId, Money price)
    {
        var lineItem = LineItem.Create(
            LineItemId.Create(Guid.NewGuid()),
            Id,
            productId,
            price);
        _lineItems.Add(lineItem);
        
        Raise(new LineItemAddedDomainEvent(Guid.NewGuid(), Id, lineItem.Id));
    }

    public void RemoveLineItem(LineItemId lineItemId)
    {
        if (_lineItems.Count == 1)
        {
            throw new HasOneLineItemDomainException(Id);
        }
        
        var lineItem = _lineItems.FirstOrDefault(li => li.Id == lineItemId);
        
        if (lineItem is null)
        {
            throw new LineItemNotFoundDomainException(Id, lineItemId);
        }

        _lineItems.Remove(lineItem);
        
        Raise(new LineItemRemovedDomainEvent(Guid.NewGuid(), Id, lineItem.Id));
    }
}