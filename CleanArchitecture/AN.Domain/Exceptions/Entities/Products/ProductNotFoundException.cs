using AN.Domain.Entities.Products;
using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Products;

public sealed class ProductNotFoundException(ProductId id) : DomainException($"Product with ID '{id.Value}' does not exist");