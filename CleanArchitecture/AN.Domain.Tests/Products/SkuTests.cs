using AN.Domain.Entities.Products;
using AN.Domain.Exceptions.Entities.Products;
using FluentAssertions;

namespace AN.Domain.Tests.Products;

public class SkuTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WhenEmptyValueIsPassed_ShouldThrowEmptySkuDomainException(string value)
    {
        // Act && Assert
        var createFunction = () => Sku.Create(value);
        createFunction.Should().Throw<EmptySkuDomainException>();
    }
    
    [Theory]
    [InlineData("invalid_sku")]
    [InlineData("invalid_sku_1")]
    [InlineData("invalid_sku_2")]
    public void Create_WhenIncorrectLengthValueIsPassed_ShouldThrowInvalidSkuLengthDomainException(string value)
    {
        // Act && Assert
        var createFunction = () => Sku.Create(value);
        createFunction.Should().Throw<InvalidSkuLengthDomainException>();
    }
    
    [Fact]
    public void Create_WhenPassedCorrectSku_ShouldCreateSkuObjectSuccessfully()
    {
        // Arrange
        var value = "valid_sku_12345";
        
        // Act
        var sku = Sku.Create(value);
        
        // Assert
        sku.Should().NotBeNull();
        sku.Value.Should().Be(value);
    }
}