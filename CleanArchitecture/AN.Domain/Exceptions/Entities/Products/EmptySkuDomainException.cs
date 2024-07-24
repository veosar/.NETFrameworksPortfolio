using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Products;

public sealed class EmptySkuDomainException() : DomainException("Sku can not be empty");