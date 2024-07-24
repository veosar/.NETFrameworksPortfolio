using AN.Application.Data;
using AN.Domain.Entities.Products;
using AN.Domain.Exceptions.Entities.Products;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Products.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }
        
        _productRepository.Remove(product);
    }
}