using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Shared;

public sealed class TooLongLastNameDomainException(string value, int maxLength) : DomainException($"Last name {value} is exceeding the maximum length of {maxLength}");