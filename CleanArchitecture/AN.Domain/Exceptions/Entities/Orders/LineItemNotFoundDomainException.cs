using AN.Domain.Entities.Orders;
using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Orders;

public class LineItemNotFoundDomainException(OrderId OrderId, LineItemId LineItemId)
    : DomainException($"LineItemId with Id '{LineItemId.Value}' not found in Order '{OrderId.Value}'");