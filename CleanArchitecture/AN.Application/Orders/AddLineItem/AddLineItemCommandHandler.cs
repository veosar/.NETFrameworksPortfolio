using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Orders;
using AN.Domain.Exceptions.Entities.Products;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Orders.AddLineItem;

internal sealed class AddLineItemCommandHandler : IRequestHandler<AddLineItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public AddLineItemCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task Handle(AddLineItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);

        if (order is null)
        {
            throw new OrderNotFoundDomainException(request.OrderId);
        }

        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }
        
        order.AddLineItem(product.Id, Money.Create(request.CurrencyId, request.Amount));
        _orderRepository.Update(order);
    }
}