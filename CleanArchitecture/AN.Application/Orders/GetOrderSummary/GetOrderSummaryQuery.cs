using AN.Domain.Entities.Orders;
using MediatR;

namespace AN.Application.Orders.GetOrderSummary;

public record GetOrderSummaryQuery(OrderId OrderId) : IRequest<OrderSummary>;