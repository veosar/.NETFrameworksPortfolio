namespace AN.WebApi.Contracts.Orders;

public record AddLineItemRequest(Guid ProductId, Guid CurrencyId, decimal Amount);