using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Orders;
using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Orders;
using AN.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;

namespace AN.Domain.Tests.Orders;

public class OrderTests
{
    [Fact]
    public void Create_WhenPassedWithCorrectData_ShouldCreateSuccessfullyAndRaisesOrderCreatedDomainEvent()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        // Act
        var order = Order.Create(CustomerId.Create(customerId));
        // Assert
        order.Id.Value.Should().NotBeEmpty();
        order.CustomerId.Value.Should().Be(customerId);
        order.GetDomainEvents().OfType<OrderCreatedDomainEvent>().Should().NotBeEmpty();
        order.LineItems.Should().BeEmpty();
    }
    
    [Fact]
    public void Add_WhenInvoked_ProperlyAddsLineItemToListAndRaisesLineItemAddedDomainEvent()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var currencyId = Guid.NewGuid();
        var amount = 5m;
        var order = Order.Create(CustomerId.Create(customerId));
        // Act && Assert
        order.LineItems.Should().BeEmpty();
        order.AddLineItem(ProductId.Create(productId), Money.Create(CurrencyId.Create(currencyId), amount));
        order.LineItems.Should().NotBeEmpty();
        var lineItem = order.LineItems.Single();
        lineItem.OrderId.Value.Should().NotBeEmpty();
        lineItem.ProductId.Value.Should().Be(productId);
        lineItem.Price.CurrencyId.Value.Should().Be(currencyId);
        lineItem.Price.Amount.Should().Be(amount);

        order.GetDomainEvents().OfType<LineItemAddedDomainEvent>().Should().NotBeEmpty();
    }
    
    [Fact]
    public void RemoveLineItem_WhenInvokedAndThereIsMoreThanOneLineItem_ProperlyRemovesLineItemFromListAndRaisesLineItemRemovedDomainEvent()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var currencyId = Guid.NewGuid();
        var amount = 5m;
        var order = Order.Create(CustomerId.Create(customerId));
        // Act && Assert
        order.LineItems.Should().BeEmpty();
        order.AddLineItem(ProductId.Create(productId), Money.Create(CurrencyId.Create(currencyId), amount));
        order.AddLineItem(ProductId.Create(productId), Money.Create(CurrencyId.Create(currencyId), amount));
        order.LineItems.Count.Should().Be(2);
        var lineItem = order.LineItems.First();
        var lineItemId = lineItem.Id;
        order.RemoveLineItem(lineItemId);
        order.LineItems.Count.Should().Be(1);
        order.GetDomainEvents().OfType<LineItemRemovedDomainEvent>().Should().NotBeEmpty();
    }
    
    [Fact]
    public void RemoveLineItem_WhenInvokedAndThereIsOneLineItem_ThrowsHasOneLineItemDomainException()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var currencyId = Guid.NewGuid();
        var amount = 5m;
        var order = Order.Create(CustomerId.Create(customerId));
        // Act && Assert
        order.LineItems.Should().BeEmpty();
        order.AddLineItem(ProductId.Create(productId), Money.Create(CurrencyId.Create(currencyId), amount));
        order.LineItems.Should().NotBeEmpty();
        var lineItem = order.LineItems.Single();
        var lineItemId = lineItem.Id;
        var removeLineItemFunction = () => order.RemoveLineItem(lineItemId);
        removeLineItemFunction.Should().Throw<HasOneLineItemDomainException>();
        order.LineItems.Should().NotBeEmpty();
        order.GetDomainEvents().OfType<LineItemRemovedDomainEvent>().Should().BeEmpty();
    }
    
    [Fact]
    public void RemoveLineItem_WhenThereIsNoSuchLineItem_ThrowsLineItemNotFoundDomainException()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var currencyId = Guid.NewGuid();
        var amount = 5m;
        var order = Order.Create(CustomerId.Create(customerId));
        // Act && Assert
        order.LineItems.Should().BeEmpty();
        order.AddLineItem(ProductId.Create(productId), Money.Create(CurrencyId.Create(currencyId), amount));
        order.AddLineItem(ProductId.Create(productId), Money.Create(CurrencyId.Create(currencyId), amount));
        order.LineItems.Should().NotBeEmpty();
        var removeLineItemFunction = () => order.RemoveLineItem(LineItemId.Create(Guid.NewGuid()));
        removeLineItemFunction.Should().Throw<LineItemNotFoundDomainException>();
        order.LineItems.Should().NotBeEmpty();
    }
}