using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Shared;
using FluentAssertions;

namespace AN.Domain.Tests.Shared;

public class MoneyTests
{
    [Fact]
    public void Create_WhenPassedNegativeAmount_ShouldThrowNegativeAmountDomainException()
    {
        // Arrange
        var currencyId = Guid.NewGuid();
        var amount = -2m;
        
        // Act && Assert
        var createMoneyFunction = () => Money.Create(CurrencyId.Create(currencyId), amount);
        createMoneyFunction.Should().Throw<NegativeAmountDomainException>();
    }
    
    [Fact]
    public void Create_WhenPassedCorrectData_ShouldSuccessfullyCreateMoneyObject()
    {
        // Arrange
        var currencyId = Guid.NewGuid();
        var amount = 2m;
        
        // Act
        var money = Money.Create(CurrencyId.Create(currencyId), amount);
        
        // Assert
        money.CurrencyId.Value.Should().Be(currencyId);
        money.Amount.Should().Be(amount);
    }
}