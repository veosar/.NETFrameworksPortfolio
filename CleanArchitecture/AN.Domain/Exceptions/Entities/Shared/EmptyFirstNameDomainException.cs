using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Shared;

public sealed class EmptyFirstNameDomainException() : DomainException("First name can not be empty");