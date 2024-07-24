using AN.Application.Currencies.Get;
using MediatR;

namespace AN.Application.Currencies.GetAll;

public record GetAllCurrenciesQuery : IRequest<List<CurrencyResponse>>;