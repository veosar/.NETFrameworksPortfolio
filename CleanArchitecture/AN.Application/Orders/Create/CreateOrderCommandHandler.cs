using AN.Application.Data;
using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Orders;
using AN.Domain.Exceptions.Entities.Customers;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Orders.Create;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderId>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderSummaryRepository _orderSummaryRepository;
    public CreateOrderCommandHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository, IOrderSummaryRepository orderSummaryRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _orderSummaryRepository = orderSummaryRepository;
    }

    public async Task<OrderId> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
        {
            throw new CustomerNotFoundDomainException(request.CustomerId);
        }

        var order = Order.Create(customer.Id);

        _orderRepository.Add(order);
        _orderSummaryRepository.Add(new OrderSummary(order.Id.Value, customer.Id.Value, 0));

        return order.Id;
    }
}