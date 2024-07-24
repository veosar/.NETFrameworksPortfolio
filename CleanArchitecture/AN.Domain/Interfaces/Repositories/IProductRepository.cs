using AN.Domain.Entities.Products;

namespace AN.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(ProductId id);
    void Add(Product product);
    void Update(Product product);
    void Remove(Product product);
}