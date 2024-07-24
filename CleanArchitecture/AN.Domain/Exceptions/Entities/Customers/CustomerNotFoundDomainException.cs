using AN.Domain.Entities.Customers;
using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Customers;

public class CustomerNotFoundDomainException(CustomerId Id) : DomainException($"Customer with ID '{Id} was not found'");