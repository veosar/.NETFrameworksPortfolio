using AN.Application.Customers.Get;
using AN.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AN.Application.Customers.GetAll;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllCustomersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _context
            .Customers
            .Select(c => new CustomerResponse(c.Id.Value, c.Email.Value, c.FirstName.Value, c.LastName.Value))
            .ToListAsync(cancellationToken);

        return customers;
    }
}