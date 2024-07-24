using System.Diagnostics.CodeAnalysis;
using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;

namespace AN.Domain.Entities.Orders;

public class LineItem
{
    [ExcludeFromCodeCoverage]
    private LineItem()
    {
        
    }
    private LineItem(LineItemId id, OrderId orderId, ProductId productId, Money price)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Price = price;
    }
    public LineItemId Id { get; private set; }
    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public Money Price { get; private set; }

    public static LineItem Create(LineItemId id, OrderId orderId, ProductId productId, Money price)
    {
        return new LineItem(id, orderId, productId, price);
    }
}