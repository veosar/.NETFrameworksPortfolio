using AN.Domain.Exceptions.Entities.Shared;

namespace AN.Domain.Entities.Shared;

public record LastName
{
    private const int MaxLength = 50;
    private LastName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static LastName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyLastNameDomainException();
        }

        if (value.Length > MaxLength)
        {
            throw new TooLongLastNameDomainException(value, MaxLength);
        }

        return new LastName(value);
    }
}