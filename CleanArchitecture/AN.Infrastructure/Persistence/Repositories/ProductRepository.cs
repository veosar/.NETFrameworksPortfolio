using AN.Domain.Entities.Products;
using AN.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AN.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(ProductId id)
    {
        var product = await _context
            .Products
            .FirstOrDefaultAsync(p => p.Id == id);

        return product;
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }

    public void Update(Product product)
    {
        // This is unnecessary in EntityFramework because the changes to entity will be tracked automatically
        // _context.Products.Update(product);
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}