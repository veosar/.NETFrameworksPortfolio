using AN.Application.Data;
using AN.Domain.Exceptions.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AN.Application.Products.Get;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponse>
{
    private readonly IApplicationDbContext _context;

    public GetProductQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context
            .Products
            .Where(p => p.Id == request.ProductId)
            .Select(p => new ProductResponse(
                p.Id.Value,
                p.Name.Value,
                p.Sku.Value,
                _context.Currencies
                    .Where(c => c.Id == p.Price.CurrencyId)
                    .Select(c => c.Name.Value)
                    .First(),
                p.Price.Amount
            )).FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        return product;
    }
}