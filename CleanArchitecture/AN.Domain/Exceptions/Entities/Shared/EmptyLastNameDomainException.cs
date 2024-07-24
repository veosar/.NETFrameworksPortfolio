using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Shared;

public sealed class EmptyLastNameDomainException() : DomainException("Last name can not be empty");