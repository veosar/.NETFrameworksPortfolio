using AN.Domain.Exceptions.Entities.Shared;

namespace AN.Domain.Entities.Shared;

public record CurrencyId
{
    public Guid Value { get; init; }

    private CurrencyId(Guid value)
    {
        Value = value;
    }

    public static CurrencyId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new CurrencyIdEmptyDomainException();
        }

        return new CurrencyId(value);
    }
}