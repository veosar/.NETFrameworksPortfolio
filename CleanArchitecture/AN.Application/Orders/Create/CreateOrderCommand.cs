using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Orders;
using MediatR;

namespace AN.Application.Orders.Create;

public record CreateOrderCommand(CustomerId CustomerId) : IRequest<OrderId>;