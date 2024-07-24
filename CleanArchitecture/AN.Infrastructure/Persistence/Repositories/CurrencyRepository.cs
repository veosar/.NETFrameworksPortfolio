using AN.Domain.Entities.Shared;
using AN.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AN.Infrastructure.Persistence.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationDbContext _context;

    public CurrencyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Currency?> GetByIdAsync(CurrencyId id)
    {
        var currency = await _context.Currencies
            .FirstOrDefaultAsync(c => c.Id == id);

        return currency;
    }

    public void Add(Currency currency)
    {
        _context.Add(currency);
    }

    public void Update(Currency currency)
    {
        // This is not necessary in EF since we're tracking all changes
        // _context.Update(currency);
    }

    public void Delete(Currency currency)
    {
        _context.Remove(currency);
    }
}