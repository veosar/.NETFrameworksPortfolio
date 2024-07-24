using AN.Domain.Exceptions.Entities.Orders;

namespace AN.Domain.Entities.Orders;

public record OrderId
{
    public Guid Value { get; init; }

    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new OrderIdEmptyDomainException();
        }

        return new OrderId(value);
    }
}