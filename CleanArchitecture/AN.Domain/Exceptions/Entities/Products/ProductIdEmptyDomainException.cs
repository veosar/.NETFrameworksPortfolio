using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Products;

public sealed class ProductIdEmptyDomainException() : DomainException("Product Id can not be empty");