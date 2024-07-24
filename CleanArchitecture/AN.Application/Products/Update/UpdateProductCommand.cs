using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using MediatR;

namespace AN.Application.Products.Update;

public record UpdateProductCommand(ProductId ProductId, ProductName Name, Sku Sku, CurrencyId CurrencyId, decimal Amount) : IRequest;