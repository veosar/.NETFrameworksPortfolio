using AN.Domain.Entities.Orders;
using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Orders;

public class OrderNotFoundDomainException(OrderId Id) : DomainException($"Order with Id '{Id.Value}' was not found");