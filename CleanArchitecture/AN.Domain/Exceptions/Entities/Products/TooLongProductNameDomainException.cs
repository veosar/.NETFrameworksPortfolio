using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Products;

public sealed class TooLongProductNameDomainException(string productName, int maxLength) : DomainException($"Product Name '{productName}' exceeded maximum length of {maxLength}");