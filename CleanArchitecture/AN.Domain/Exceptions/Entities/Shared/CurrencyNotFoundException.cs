using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Shared;

public class CurrencyNotFoundException(CurrencyId Id) : DomainException($"Currency with Id '{Id}' could not be found");