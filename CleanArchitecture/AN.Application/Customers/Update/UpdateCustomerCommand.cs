using AN.Application.Messaging;
using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Shared;
using MediatR;

namespace AN.Application.Customers.Update;

public record UpdateCustomerCommand(CustomerId Id, Email Email, FirstName FirstName, LastName LastName) : ICommand;