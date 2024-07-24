using AN.Domain.Entities.Orders;
using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using FluentAssertions;

namespace AN.Domain.Tests.Orders;

public class LineItemTests
{
    [Fact]
    public void Create_WhenPassedCorrectData_SuccessfullyCreatesLineItemObject()
    {
        // Arrange
        var lineItemId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var price = 5m;
        var currencyId = Guid.NewGuid();
            
        // Act
        var lineItem = LineItem.Create(LineItemId.Create(lineItemId), OrderId.Create(orderId),
            ProductId.Create(productId), Money.Create(CurrencyId.Create(currencyId), price));

        lineItem.Id.Value.Should().Be(lineItemId);
        lineItem.OrderId.Value.Should().Be(orderId);
        lineItem.ProductId.Value.Should().Be(productId);
        lineItem.Price.CurrencyId.Value.Should().Be(currencyId);
        lineItem.Price.Amount.Should().Be(price);

    }
}