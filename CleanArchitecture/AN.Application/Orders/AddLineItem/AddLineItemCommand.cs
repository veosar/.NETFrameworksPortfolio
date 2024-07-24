using AN.Domain.Entities.Orders;
using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using MediatR;

namespace AN.Application.Orders.AddLineItem;

public record AddLineItemCommand(OrderId OrderId, ProductId ProductId, CurrencyId CurrencyId, decimal Amount) : IRequest;