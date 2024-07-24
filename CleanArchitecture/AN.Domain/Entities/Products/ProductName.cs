using AN.Domain.Exceptions.Entities.Products;

namespace AN.Domain.Entities.Products;

public class ProductName
{
    private const int MaxLength = 255;
    private ProductName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static ProductName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyProductNameDomainException();
        }

        if (value.Length > MaxLength)
        {
            throw new TooLongProductNameDomainException(value, MaxLength);
        }

        return new ProductName(value);
    }
}