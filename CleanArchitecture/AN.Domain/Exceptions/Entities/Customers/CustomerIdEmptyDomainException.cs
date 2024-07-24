using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Customers;

public sealed class CustomerIdEmptyDomainException() : DomainException("Customer Id can not be empty");