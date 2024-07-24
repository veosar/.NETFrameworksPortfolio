using System.Diagnostics.CodeAnalysis;
using AN.Domain.Entities.Primitives;

namespace AN.Domain.Entities.Orders;

[ExcludeFromCodeCoverage]
public record LineItemRemovedDomainEvent(Guid Id, OrderId OrderId, LineItemId LineItemId) : DomainEvent(Id);