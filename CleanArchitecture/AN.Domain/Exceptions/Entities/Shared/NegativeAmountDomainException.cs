using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Shared;

public sealed class NegativeAmountDomainException() : DomainException("Amount can not be a negative number");