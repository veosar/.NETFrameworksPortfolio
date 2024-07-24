using System.Diagnostics.CodeAnalysis;
using AN.Domain.Entities.Primitives;

namespace AN.Domain.Entities.Orders;

[ExcludeFromCodeCoverage]
public record OrderCreatedDomainEvent(Guid Id, OrderId OrderId) : DomainEvent(Id);