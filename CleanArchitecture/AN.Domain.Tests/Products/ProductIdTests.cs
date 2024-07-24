using AN.Domain.Entities.Products;
using AN.Domain.Exceptions.Entities.Products;
using FluentAssertions;

namespace AN.Domain.Tests.Products;

public class ProductIdTests
{
    [Fact]
    public void Create_WhenPassedEmptyGuid_ThrowsProductIdEmptyDomainException()
    {
        // Arrange
        Guid guid = Guid.Empty;
        
        // Act && Assert
        var createProductIdFunction = () => ProductId.Create(guid);
        createProductIdFunction.Should().Throw<ProductIdEmptyDomainException>();
    }
    
    [Fact]
    public void Create_WhenPassedCorrectGuid_SuccessfullyCreatesProductId()
    {
        // Arrange
        Guid guid = Guid.NewGuid();
        
        // Act
        var productId = ProductId.Create(guid);
        productId.Value.Should().Be(guid);
    }
}