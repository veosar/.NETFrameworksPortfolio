using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Shared;
using FluentAssertions;

namespace AN.Domain.Tests.Customers;

public class CustomerTests
{
    [Fact]
    public void Create_WhenInvokedWithProperData_SuccessfullyCreatesCustomerObject()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var email = "adam.nowak@angrynerds.co";
        var firstName = "Adam";
        var lastName = "Nowak";
        
        // Act
        var customer = Customer.Create(CustomerId.Create(customerId), Email.Create(email), FirstName.Create(firstName),
            LastName.Create(lastName));

        // Assert
        customer.Id.Value.Should().Be(customerId);
        customer.Email.Value.Should().Be(email);
        customer.FirstName.Value.Should().Be(firstName);
        customer.LastName.Value.Should().Be(lastName);
    }
}