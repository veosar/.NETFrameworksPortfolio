using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Shared;

public sealed class CurrencyIdEmptyDomainException() : DomainException("Currency Id can not be empty");