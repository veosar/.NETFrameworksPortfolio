using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using FluentAssertions;

namespace AN.Domain.Tests.Products;

public class ProductTests
{
    [Fact]
    public void Create_WhenPassedCorrectData_SuccessfullyCreatesProductObject()
    {
        // Arrange
        var productName = "SampleProductName";
        var productId = Guid.NewGuid();
        var price = 5m;
        var currencyId = Guid.NewGuid();
        var sku = "sample-sku-1234";
            
        // Act
        var product = Product.Create(ProductId.Create(productId), ProductName.Create(productName),
            Money.Create(CurrencyId.Create(currencyId), price), Sku.Create(sku));

        product.Id.Value.Should().Be(productId);
        product.Name.Value.Should().Be(productName);
        product.Sku.Value.Should().Be(sku);
        product.Price.CurrencyId.Value.Should().Be(currencyId);
        product.Price.Amount.Should().Be(price);

    }
    
    [Fact]
    public void Update_WhenPassedCorrectData_SuccessfullyUpdatesProductObject()
    {
        // Arrange
        var productName = "SampleProductName";
        var newProductName = "SampleProductName2";
        var productId = Guid.NewGuid();
        var price = 5m;
        var newPrice = 6m;
        var currencyId = Guid.NewGuid();
        var newCurrencyId = Guid.NewGuid();
        var sku = "sample-sku-1234";
        var newSku = "sample-sku-1235";
            
        // Act
        var product = Product.Create(ProductId.Create(productId), ProductName.Create(productName),
            Money.Create(CurrencyId.Create(currencyId), price), Sku.Create(sku));

        product.Id.Value.Should().Be(productId);
        product.Name.Value.Should().Be(productName);
        product.Sku.Value.Should().Be(sku);
        product.Price.CurrencyId.Value.Should().Be(currencyId);
        product.Price.Amount.Should().Be(price);
        
        product.Update(ProductName.Create(newProductName), Money.Create(CurrencyId.Create(newCurrencyId), newPrice),
            Sku.Create(newSku));
        
        product.Id.Value.Should().Be(productId);
        product.Name.Value.Should().Be(newProductName);
        product.Sku.Value.Should().Be(newSku);
        product.Price.CurrencyId.Value.Should().Be(newCurrencyId);
        product.Price.Amount.Should().Be(newPrice);

    }
}