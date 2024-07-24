using AN.Domain.Entities.Products;
using MediatR;

namespace AN.Application.Products.Get;

public record GetProductQuery(ProductId ProductId) : IRequest<ProductResponse>;