using System.Diagnostics.CodeAnalysis;

namespace AN.Domain.Entities.Orders;

[ExcludeFromCodeCoverage]
public record OrderSummary(Guid Id, Guid CustomerId, decimal TotalPrice);