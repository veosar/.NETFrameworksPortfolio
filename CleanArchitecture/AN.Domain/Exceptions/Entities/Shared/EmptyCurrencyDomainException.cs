using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Shared;

public sealed class EmptyCurrencyDomainException() : DomainException("Currency can not be empty");