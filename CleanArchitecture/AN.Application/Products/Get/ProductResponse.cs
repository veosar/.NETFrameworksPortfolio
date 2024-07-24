namespace AN.Application.Products.Get;

public record ProductResponse(Guid Id, string Name, string Sku, string Currency, decimal Amount);