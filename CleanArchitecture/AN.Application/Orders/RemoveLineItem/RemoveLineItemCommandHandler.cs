using AN.Application.Data;
using AN.Domain.Exceptions.Entities.Orders;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Orders.RemoveLineItem;

internal sealed class RemoveLineItemCommandHandler : IRequestHandler<RemoveLineItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public RemoveLineItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
       
        if (order is null)
        {
            throw new OrderNotFoundDomainException(request.OrderId);
        }
        
        order.RemoveLineItem(request.LineItemId);
        _orderRepository.Update(order);
    }
}