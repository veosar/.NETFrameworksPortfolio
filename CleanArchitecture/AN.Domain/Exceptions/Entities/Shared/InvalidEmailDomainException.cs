using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Shared;

public sealed class InvalidEmailDomainException(string email, Exception exception) : DomainException($"{email} is not a valid email", exception);