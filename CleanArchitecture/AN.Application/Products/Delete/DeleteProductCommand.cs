using AN.Domain.Entities.Products;
using MediatR;

namespace AN.Application.Products.Delete;

public record DeleteProductCommand(ProductId ProductId) : IRequest;