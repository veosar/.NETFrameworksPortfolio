using AN.Domain.Exceptions.Entities.Customers;

namespace AN.Domain.Entities.Customers;

public record CustomerId
{
    public Guid Value { get; init; }

    private CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new CustomerIdEmptyDomainException();
        }

        return new CustomerId(value);
    }
}