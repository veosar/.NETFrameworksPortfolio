using AN.Domain.Entities.Shared;
using MediatR;

namespace AN.Application.Currencies.Delete;

public record DeleteCurrencyCommand(CurrencyId Id) : IRequest;