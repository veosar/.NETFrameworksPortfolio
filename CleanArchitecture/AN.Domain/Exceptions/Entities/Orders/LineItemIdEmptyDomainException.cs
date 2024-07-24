using System.Diagnostics.CodeAnalysis;
using AN.Domain.Exceptions.Base;

namespace AN.Domain.Exceptions.Entities.Orders;

[ExcludeFromCodeCoverage]
public sealed class LineItemIdEmptyDomainException() : DomainException("Line Item Id can not be empty");