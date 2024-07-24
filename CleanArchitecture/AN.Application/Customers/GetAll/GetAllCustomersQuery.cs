using AN.Application.Customers.Get;
using MediatR;

namespace AN.Application.Customers.GetAll;

public record GetAllCustomersQuery() : IRequest<List<CustomerResponse>>;