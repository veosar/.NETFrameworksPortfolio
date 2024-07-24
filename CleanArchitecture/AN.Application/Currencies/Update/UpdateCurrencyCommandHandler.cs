using AN.Application.Data;
using AN.Domain.Exceptions.Entities.Shared;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Currencies.Update;

public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand>
{
    private readonly ICurrencyRepository _currencyRepository;

    public UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = await _currencyRepository.GetByIdAsync(request.Id);

        if (currency is null)
        {
            throw new CurrencyNotFoundException(request.Id);
        }

        currency.Update(request.Name);

        _currencyRepository.Update(currency);
    }
}