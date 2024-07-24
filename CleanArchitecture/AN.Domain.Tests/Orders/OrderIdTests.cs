using AN.Domain.Entities.Orders;
using AN.Domain.Exceptions.Entities.Orders;
using FluentAssertions;

namespace AN.Domain.Tests.Orders;

public class OrderIdTests
{
    [Fact]
    public void Create_WhenPassedEmptyGuid_ThrowsOrderIdEmptyDomainException()
    {
        // Arrange
        Guid guid = Guid.Empty;
        
        // Act && Assert
        var orderIdCreateFunction = () => OrderId.Create(guid);
        orderIdCreateFunction.Should().Throw<OrderIdEmptyDomainException>();
    }
    
    [Fact]
    public void Create_WhenPassedCorrectGuid_SuccessfullyCreatesOrderId()
    {
        // Arrange
        Guid guid = Guid.NewGuid();
        
        // Act
        var orderId = OrderId.Create(guid);
        orderId.Value.Should().Be(guid);
    }
}