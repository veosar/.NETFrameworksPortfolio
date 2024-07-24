using AN.Domain.Entities.Products;
using AN.Domain.Exceptions.Entities.Products;
using FluentAssertions;

namespace AN.Domain.Tests.Products;

public class ProductNameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WhenPassedEmptyString_ThrowsEmptyProductNameDomainException(string value)
    {
        // Act && Assert
        var createProductNameFunction = () => ProductName.Create(value);
        createProductNameFunction.Should().Throw<EmptyProductNameDomainException>();
    }
    
    [Fact]
    public void Create_WhenProductNameIsTooLong_ThrowsTooLongProductNameDomainException()
    {
        // Arrange
        var value = new string('*', 256);
        // Act && Assert
        var createProductNameFunction = () => ProductName.Create(value);
        createProductNameFunction.Should().Throw<TooLongProductNameDomainException>();
    }
    
    [Fact]
    public void Create_WhenPassedCorrectProductName_CreatesProductNameSuccessfully()
    {
        // Arrange
        var value = "SampleProductName";
        // Act
        var productName = ProductName.Create(value);
        productName.Value.Should().Be(value);
    }
}