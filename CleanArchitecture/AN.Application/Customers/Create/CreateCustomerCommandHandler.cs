using AN.Domain.Entities.Customers;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerId>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<CustomerId> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(CustomerId.Create(Guid.NewGuid()), request.Email, request.FirstName, request.LastName);
        _customerRepository.Add(customer);
        return Task.FromResult(customer.Id);
    }
}