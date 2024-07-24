using AN.Domain.Entities.Primitives;

namespace AN.Domain.Entities.Orders;

public sealed record LineItemAddedDomainEvent(Guid Id, OrderId OrderId, LineItemId LineItemId) : DomainEvent(Id);