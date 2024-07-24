using AN.Domain.Entities.Orders;
using AN.Domain.Exceptions.Entities.Orders;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Orders.GetOrderSummary;

internal sealed class GetOrderSummaryQueryHandler : IRequestHandler<GetOrderSummaryQuery, OrderSummary>
{
    private readonly IOrderSummaryRepository _orderSummaryRepository;

    public GetOrderSummaryQueryHandler(IOrderSummaryRepository orderSummaryRepository)
    {
        _orderSummaryRepository = orderSummaryRepository;
    }

    public async Task<OrderSummary> Handle(GetOrderSummaryQuery request, CancellationToken cancellationToken)
    {
        var orderSummary = await _orderSummaryRepository.GetByIdAsync(request.OrderId);

        if (orderSummary is null)
        {
            throw new OrderNotFoundDomainException(request.OrderId);
        }
        return orderSummary;
    }
}