using AN.Application.Data;
using AN.Domain.Entities.Shared;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Currencies.Create;

public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, CurrencyId>
{
    private readonly ICurrencyRepository _currencyRepository;

    public CreateCurrencyCommandHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public Task<CurrencyId> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = Currency.Create(CurrencyId.Create(Guid.NewGuid()), request.Name);
        _currencyRepository.Add(currency);
        return Task.FromResult(currency.Id);
    }
}