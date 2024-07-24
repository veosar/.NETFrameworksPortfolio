using AN.Application.Data;
using AN.Application.Products.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AN.Application.Products.GetAll;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context
            .Products
            .Select(p => new ProductResponse(
                p.Id.Value,
                p.Name.Value,
                p.Sku.Value,
                _context.Currencies
                    .Where(c => c.Id == p.Price.CurrencyId)
                    .Select(c => c.Name.Value)
                    .First(),
                p.Price.Amount
            )).ToListAsync(cancellationToken);

        return products;
    }
}