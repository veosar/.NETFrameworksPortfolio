using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Shared;
public sealed class CurrencyLengthTooLongDomainException(string currency, int maxLength) : DomainException($"Currency '{currency}' exceeded maximum length of {maxLength}");