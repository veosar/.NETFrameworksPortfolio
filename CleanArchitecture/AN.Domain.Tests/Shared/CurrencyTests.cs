using AN.Domain.Entities.Shared;
using FluentAssertions;

namespace AN.Domain.Tests.Shared;

public class CurrencyTests
{
    [Fact]
    public void Create_WhenPassedCorrectData_ShouldSuccessfullyCreateCurrencyObject()
    {
        // Arrange
        var currencyName = "PLN";
        var currencyId = Guid.NewGuid();
        
        // Act
        var currency = Currency.Create(CurrencyId.Create(currencyId), CurrencyName.Create(currencyName));
        
        // Assert
        currency.Id.Value.Should().Be(currencyId);
        currency.Name.Value.Should().Be(currencyName);
    }
}