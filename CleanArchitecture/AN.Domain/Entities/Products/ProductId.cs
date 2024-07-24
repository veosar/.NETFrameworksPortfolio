using AN.Domain.Exceptions.Entities.Products;

namespace AN.Domain.Entities.Products;

public record ProductId
{
    public Guid Value { get; init; }

    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ProductIdEmptyDomainException();
        }

        return new ProductId(value);
    }
}