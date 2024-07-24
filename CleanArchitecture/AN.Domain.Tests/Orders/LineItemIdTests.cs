using AN.Domain.Entities.Orders;
using AN.Domain.Exceptions.Entities.Orders;
using FluentAssertions;

namespace AN.Domain.Tests.Orders;

public class LineItemIdTests
{
    [Fact]
    public void Create_WhenPassedEmptyGuid_ThrowsLineItemIdEmptyDomainException()
    {
        // Arrange
        Guid guid = Guid.Empty;
        
        // Act && Assert
        var createLineItemIdFunction = () => LineItemId.Create(guid);
        createLineItemIdFunction.Should().Throw<LineItemIdEmptyDomainException>();
    }
    
    [Fact]
    public void Create_WhenPassedCorrectGuid_SuccessfullyCreatesLineItemId()
    {
        // Arrange
        Guid guid = Guid.NewGuid();
        
        // Act
        var lineItemId = LineItemId.Create(guid);
        lineItemId.Value.Should().Be(guid);
    }
}