using AN.Application.Data;
using AN.Domain.Exceptions.Entities.Customers;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Customers.Update;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer is null)
        {
            throw new CustomerNotFoundDomainException(request.Id);
        }

        customer.Update(request.Email, request.FirstName, request.LastName);
        
        _customerRepository.Update(customer);
    }
}