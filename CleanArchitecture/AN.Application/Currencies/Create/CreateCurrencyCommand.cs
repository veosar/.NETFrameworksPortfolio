using AN.Domain.Entities.Shared;
using MediatR;

namespace AN.Application.Currencies.Create;

public record CreateCurrencyCommand(CurrencyName Name) : IRequest<CurrencyId>;