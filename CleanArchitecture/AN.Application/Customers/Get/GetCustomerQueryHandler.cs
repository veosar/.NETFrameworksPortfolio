using AN.Application.Currencies.Get;
using AN.Application.Data;
using AN.Domain.Entities.Customers;
using AN.Domain.Exceptions.Entities.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AN.Application.Customers.Get;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerResponse?>
{
    private readonly IApplicationDbContext _context;

    public GetCustomerQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerResponse?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context
            .Customers
            .Where(c => c.Id == request.Id)
            .Select(c => new CustomerResponse(c.Id.Value, c.Email.Value, c.FirstName.Value, c.LastName.Value))
            .FirstOrDefaultAsync(cancellationToken);
        
        if (customer is null)
        {
            throw new CustomerNotFoundDomainException(request.Id);
        }

        return customer;
    }
}