using AN.Application.Data;
using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Products.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductId> 
{
    private readonly IProductRepository _productRepository;
    
    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<ProductId> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(ProductId.Create(Guid.NewGuid()), request.Name,
            Money.Create(request.CurrencyId, request.Amount), request.Sku);
        
        _productRepository.Add(product);

        return Task.FromResult(product.Id);
    }
}