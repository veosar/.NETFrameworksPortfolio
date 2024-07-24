using AN.Domain.Exceptions.Entities.Products;

namespace AN.Domain.Entities.Products;

public record Sku
{
    private const int AllowedLength = 15;
    private Sku(string value)
    {
        Value = value;
    }

    public string Value { get; init; }
    public static Sku Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptySkuDomainException();
        }

        if (value.Length != AllowedLength)
        {
            throw new InvalidSkuLengthDomainException(value, AllowedLength);
        }

        return new Sku(value);
    }
}