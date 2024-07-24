using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Shared;
using FluentAssertions;

namespace AN.Domain.Tests.Shared;

public class CurrencyIdTests
{
    [Fact]
    public void Create_WhenPassedEmptyGuid_ThrowsCurrencyIdEmptyDomainException()
    {
        // Arrange
        Guid guid = Guid.Empty;
        
        // Act && Assert
        var createCurrencyIdFunction = () => CurrencyId.Create(guid);
        createCurrencyIdFunction.Should().Throw<CurrencyIdEmptyDomainException>();
    }
    
    [Fact]
    public void Create_WhenPassedCorrectGuid_SuccessfullyCreatesCurrencyId()
    {
        // Arrange
        Guid guid = Guid.NewGuid();
        
        // Act
        var currencyId = CurrencyId.Create(guid);
        currencyId.Value.Should().Be(guid);
    }
}