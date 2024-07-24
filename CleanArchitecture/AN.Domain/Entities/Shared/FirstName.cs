using AN.Domain.Exceptions.Entities.Shared;

namespace AN.Domain.Entities.Shared;

public record FirstName
{
    private const int MaxLength = 50;
    private FirstName(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static FirstName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyFirstNameDomainException();
        }

        if (value.Length > MaxLength)
        {
            throw new TooLongFirstNameDomainException(value, MaxLength);
        }

        return new FirstName(value);
    }
}