using AN.Domain.Entities.Orders;
using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Orders;

public class HasOneLineItemDomainException(OrderId OrderId) : DomainException($"Order '{OrderId.Value}' has one line item, so you can't remove it");