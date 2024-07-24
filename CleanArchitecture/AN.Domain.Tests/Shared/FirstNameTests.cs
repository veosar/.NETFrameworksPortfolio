
using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Shared;
using FluentAssertions;

namespace AN.Domain.Tests.Shared;

public class FirstNameTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Create_WhenPassedEmptyString_ThrowsEmptyFirstNameDomainException(string value)
    {
        // Act && Assert
        var createFirstNameFunction = () => FirstName.Create(value);
        createFirstNameFunction.Should().Throw<EmptyFirstNameDomainException>();
    }

    [Fact]
    public void Create_WhenPassedTooLongName_ShouldThrowTooLongFirstNameDomainException()
    {
        // Arrange
        var value = new string('*', 51);
        
        // Act && Assert
        var createFirstNameFunction = () => FirstName.Create(value);
        createFirstNameFunction.Should().Throw<TooLongFirstNameDomainException>();
    }
}