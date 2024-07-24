using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using MediatR;

namespace AN.Application.Products.Create;

public record CreateProductCommand(ProductName Name, Sku Sku, CurrencyId CurrencyId, decimal Amount) : IRequest<ProductId>;