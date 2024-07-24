using AN.Domain.Entities.Shared;
using MediatR;

namespace AN.Application.Currencies.Update;

public record UpdateCurrencyCommand(CurrencyId Id, CurrencyName Name) : IRequest;