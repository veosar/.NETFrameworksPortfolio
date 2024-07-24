namespace AN.WebApi.Contracts.Products;

public record CreateProductRequest(string Name, string Sku, Guid CurrencyId, decimal Amount);