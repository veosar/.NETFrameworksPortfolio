using AN.Domain.Entities.Orders;
using MediatR;

namespace AN.Application.Orders.RemoveLineItem;

public record RemoveLineItemCommand(OrderId OrderId, LineItemId LineItemId) : IRequest;