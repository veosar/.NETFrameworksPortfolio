using AN.Domain.Exceptions.Entities.Shared;

namespace AN.Domain.Entities.Shared;

public class CurrencyName
{
    private const int CurrencyMaxLength = 3;
    private CurrencyName(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static CurrencyName Create(string currency)
    {
        if (string.IsNullOrEmpty(currency))
        {
            throw new EmptyCurrencyDomainException();
        }

        if (currency.Length > CurrencyMaxLength)
        {
            throw new CurrencyLengthTooLongDomainException(currency, CurrencyMaxLength);
        }
        
        return new CurrencyName(currency);
    }
}