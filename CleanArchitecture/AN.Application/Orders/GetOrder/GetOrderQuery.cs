using AN.Domain.Entities.Orders;
using MediatR;

namespace AN.Application.Orders.GetOrder;

public record GetOrderQuery(OrderId OrderId) : IRequest<OrderResponse>;