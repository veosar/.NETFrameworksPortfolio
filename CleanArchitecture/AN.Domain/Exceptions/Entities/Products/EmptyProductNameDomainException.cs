using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Products;

public sealed class EmptyProductNameDomainException() : DomainException("Product Name can not be empty");