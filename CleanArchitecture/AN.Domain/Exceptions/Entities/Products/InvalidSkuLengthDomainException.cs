using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Products;

public sealed class InvalidSkuLengthDomainException(string skuValue, int allowedLength) : DomainException($"Sku value '{skuValue}' does not have a length of {allowedLength}");