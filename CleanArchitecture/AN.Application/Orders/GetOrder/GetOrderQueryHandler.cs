using AN.Application.Data;
using AN.Domain.Entities.Orders;
using AN.Domain.Exceptions.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AN.Application.Orders.GetOrder;

internal sealed class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderResponse>
{
    private readonly IApplicationDbContext _context;

    public GetOrderQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var orderResponse = await _context
            .Orders
            .Where(o => o.Id == request.OrderId)
            .Select(order => new OrderResponse(
                order.Id.Value,
                order.CustomerId.Value,
                order.LineItems.Select(li => new LineItemResponse(
                    li.Id.Value,
                    li.Price.Amount)).ToList()))
            .FirstOrDefaultAsync(cancellationToken);

        if (orderResponse is null)
        {
            throw new OrderNotFoundDomainException(request.OrderId);
        }

        return orderResponse;
    }
}