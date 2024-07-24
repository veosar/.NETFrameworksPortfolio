using AN.Domain.Exceptions.Entities.Orders;

namespace AN.Domain.Entities.Orders;

public record LineItemId
{
    public Guid Value { get; init; }

    private LineItemId(Guid value)
    {
        Value = value;
    }

    public static LineItemId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new LineItemIdEmptyDomainException();
        }

        return new LineItemId(value);
    }
}