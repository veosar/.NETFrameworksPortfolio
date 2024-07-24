using AN.Application.Currencies.Get;
using AN.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AN.Application.Currencies.GetAll;

public class GetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, List<CurrencyResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllCurrenciesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CurrencyResponse>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await _context
            .Currencies
            .Select(c => new CurrencyResponse(c.Id.Value, c.Name.Value))
            .ToListAsync(cancellationToken);

        return currencies;
    }
}