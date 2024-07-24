using AN.Application.Messaging;
using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Shared;
using MediatR;

namespace AN.Application.Customers.Create;

public record CreateCustomerCommand(Email Email, FirstName FirstName, LastName LastName) : ICommand<CustomerId>;