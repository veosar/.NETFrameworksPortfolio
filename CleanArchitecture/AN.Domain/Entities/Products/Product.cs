using System.Diagnostics.CodeAnalysis;
using AN.Domain.Entities.Shared;

namespace AN.Domain.Entities.Products;

public class Product
{
    [ExcludeFromCodeCoverage]
    private Product()
    {
        
    }
    
    private Product(ProductId id, ProductName name, Money price, Sku sku)
    {
        Id = id;
        Name = name;
        Price = price;
        Sku = sku;
    }
    public ProductId Id { get; init; }
    public ProductName Name { get; private set; }
    public Money Price { get; private set; }
    public Sku Sku { get; private set; }

    public static Product Create(ProductId id, ProductName name, Money price, Sku sku)
    {
        return new Product(id, name, price, sku);
    }

    public void Update(ProductName name, Money price, Sku sku)
    {
        Name = name;
        Price = price;
        Sku = sku;
    }
}