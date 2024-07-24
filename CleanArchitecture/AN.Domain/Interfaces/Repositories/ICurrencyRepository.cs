using AN.Domain.Entities.Shared;

namespace AN.Domain.Interfaces.Repositories;

public interface ICurrencyRepository
{
    Task<Currency?> GetByIdAsync(CurrencyId id);
    void Add(Currency currency);
    void Update(Currency currency);
    void Delete(Currency currency);
}