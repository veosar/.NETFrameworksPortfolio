using AN.Domain.Entities.Customers;
using AN.Domain.Exceptions.Entities.Customers;
using FluentAssertions;

namespace AN.Domain.Tests.Customers;

public class CustomerIdTests
{
    [Fact]
    public void Create_WhenPassedEmptyGuid_ThrowsCustomerIdEmptyDomainException()
    {
        // Arrange
        Guid guid = Guid.Empty;
        
        // Act && Assert
        var createCustomerIdFunction = () => CustomerId.Create(guid);
        createCustomerIdFunction.Should().Throw<CustomerIdEmptyDomainException>();
    }
    
    [Fact]
    public void Create_WhenPassedCorrectGuid_SuccessfullyCreatesCustomerId()
    {
        // Arrange
        Guid guid = Guid.NewGuid();
        
        // Act
        var customerId = CustomerId.Create(guid);
        customerId.Value.Should().Be(guid);
    }
}