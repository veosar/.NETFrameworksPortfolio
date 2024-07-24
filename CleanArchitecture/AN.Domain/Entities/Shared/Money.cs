using System.Diagnostics.CodeAnalysis;
using AN.Domain.Exceptions.Entities.Shared;

namespace AN.Domain.Entities.Shared;

public record Money
{
    [ExcludeFromCodeCoverage]
    private Money()
    {
        
    }
    private Money(CurrencyId currencyId, decimal amount)
    {
        CurrencyId = currencyId;
        Amount = amount;
    }

    public CurrencyId CurrencyId { get; init; }
    public decimal Amount { get; init; }
    public static Money Create(CurrencyId currencyId, decimal amount)
    {
        if (amount < 0)
        {
            throw new NegativeAmountDomainException();
        }

        return new Money(currencyId, amount);
    }
}