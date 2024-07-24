using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Shared;

public sealed class TooLongFirstNameDomainException(string value, int maxLength) : DomainException($"First name {value} is exceeding the maximum length of {maxLength}");