using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Shared;
using FluentAssertions;

namespace AN.Domain.Tests.Shared;

public class CurrencyNameTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Create_WhenPassedEmptyString_ThrowsEmptyCurrencyDomainException(string currencyName)
    {
        // Act && Assert
        var createCurrencyNameFunction = () => CurrencyName.Create(currencyName);
        createCurrencyNameFunction.Should().Throw<EmptyCurrencyDomainException>();
    }
    
    [Theory]
    [InlineData("TEST")]
    [InlineData("TEST1")]
    [InlineData("TEST12")]
    public void Create_WhenPassedTooLongString_ThrowsCurrencyLengthTooLongDomainException(string currencyName)
    {
        // Act && Assert
        var createCurrencyNameFunction = () => CurrencyName.Create(currencyName);
        createCurrencyNameFunction.Should().Throw<CurrencyLengthTooLongDomainException>();
    }

    [Fact]
    public void Create_WhenPassedCorrectData_ShouldSuccessfullyCreateCurrencyNameObject()
    {
        // Arrange
        var currencyNameValue = "PLN";
        
        // Act
        var currencyName = CurrencyName.Create(currencyNameValue);
        
        // Assert
        currencyName.Value.Should().Be(currencyNameValue);
    }
}