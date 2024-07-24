using AN.Application.Data;
using AN.Domain.Exceptions.Entities.Shared;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Currencies.Delete;

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand>
{
    private readonly ICurrencyRepository _currencyRepository;

    public DeleteCurrencyCommandHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = await _currencyRepository.GetByIdAsync(request.Id);
        if (currency is null)
        {
            throw new CurrencyNotFoundException(request.Id);
        }
        
        _currencyRepository.Delete(currency);
    }
}