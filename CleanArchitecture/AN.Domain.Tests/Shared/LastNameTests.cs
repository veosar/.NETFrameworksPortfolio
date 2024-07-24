
using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Shared;
using FluentAssertions;

namespace AN.Domain.Tests.Shared;

public class LastNameTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Create_WhenPassedEmptyString_ThrowsEmptyLastNameDomainException(string value)
    {
        // Act && Assert
        var createLastNameFunction = () => LastName.Create(value);
        createLastNameFunction.Should().Throw<EmptyLastNameDomainException>();
    }

    [Fact]
    public void Create_WhenPassedTooLongName_ShouldThrowTooLongLastNameDomainException()
    {
        // Arrange
        var value = new string('*', 51);
        
        // Act && Assert
        var createLastNameFunction = () => LastName.Create(value);
        createLastNameFunction.Should().Throw<TooLongLastNameDomainException>();
    }
}