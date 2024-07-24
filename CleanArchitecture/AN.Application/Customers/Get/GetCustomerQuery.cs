using AN.Domain.Entities.Customers;
using MediatR;

namespace AN.Application.Customers.Get;

public record GetCustomerQuery(CustomerId Id) : IRequest<CustomerResponse>;