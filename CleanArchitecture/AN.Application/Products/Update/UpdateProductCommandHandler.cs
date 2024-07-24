using AN.Application.Data;
using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Products;
using AN.Domain.Interfaces.Repositories;
using MediatR;

namespace AN.Application.Products.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        product.Update(
            request.Name,
            Money.Create(request.CurrencyId, request.Amount),
            request.Sku
            );

        _productRepository.Update(product);
    }
}