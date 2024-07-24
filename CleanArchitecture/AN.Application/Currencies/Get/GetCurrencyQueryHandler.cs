using AN.Application.Data;
using AN.Domain.Exceptions.Entities.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AN.Application.Currencies.Get;

public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, CurrencyResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCurrencyQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CurrencyResponse> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
    {
        var currency = await _context
            .Currencies
            .Where(c => c.Id == request.Id)
            .Select(c => new CurrencyResponse(c.Id.Value, c.Name.Value))
            .FirstOrDefaultAsync(cancellationToken);
        
        if (currency is null)
        {
            throw new CurrencyNotFoundException(request.Id);
        }

        return currency;
    }
}