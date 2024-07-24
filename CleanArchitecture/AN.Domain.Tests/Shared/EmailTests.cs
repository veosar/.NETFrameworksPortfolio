using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Shared;
using FluentAssertions;

namespace AN.Domain.Tests.Shared;

public class EmailTests
{
    [Theory]
    [InlineData("test")]
    [InlineData("test@")]
    [InlineData("test@@@")]
    [InlineData("")]
    [InlineData(null)]
    public void Create_WhenPassedInvalidEmailValue_ThrowsInvalidEmailDomainException(string emailValue)
    {
        // Act & Assert
        var createEmailFunction = () => Email.Create(emailValue);
        createEmailFunction.Should().Throw<InvalidEmailDomainException>();
    }

    [Fact]
    public void Create_WhenPassedCorrectEmailValue_SuccessfullyCreatesEmailObject()
    {
        // Arrange
        var emailValue = "adam.nowak@angrynerds.co";
        
        // Act
        var email = Email.Create(emailValue);
        
        // Assert
        email.Value.Should().Be(emailValue);
    }
}