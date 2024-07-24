using AN.Application.Products.Get;
using MediatR;

namespace AN.Application.Products.GetAll;

public record GetAllProductsQuery : IRequest<List<ProductResponse>>;