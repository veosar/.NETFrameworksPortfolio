using AN.Domain.Entities.Shared;
using MediatR;

namespace AN.Application.Currencies.Get;

public record GetCurrencyQuery(CurrencyId Id) : IRequest<CurrencyResponse>;