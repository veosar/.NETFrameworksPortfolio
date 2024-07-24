using AN.Application.Products.Create;
using AN.Application.Products.Delete;
using AN.Application.Products.Get;
using AN.Application.Products.GetAll;
using AN.Application.Products.Update;
using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Products;
using AN.WebApi.Contracts.Products;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AN.WebApi.Endpoints;

public class Products : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (CreateProductRequest createProductRequest, ISender sender) =>
        {
            var command = new CreateProductCommand(
                ProductName.Create(createProductRequest.Name),
                Sku.Create(createProductRequest.Sku),
                CurrencyId.Create(createProductRequest.CurrencyId),
                createProductRequest.Amount);

            var productId = await sender.Send(command);
            return Results.Created($"products/{productId}", productId);
        });

        app.MapDelete("products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var command = new DeleteProductCommand(ProductId.Create(id));
                await sender.Send(command);
                return Results.NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapGet("products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetProductQuery(ProductId.Create(id))));
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
        
        app.MapGet("products", async (ISender sender) =>
        {
            var products = await sender.Send(new GetAllProductsQuery());
            return Results.Ok(products);
        });

        app.MapPut("products/{id:guid}", async (Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            var command = new UpdateProductCommand(ProductId.Create(id), ProductName.Create(request.Name),
                Sku.Create(request.Sku), CurrencyId.Create(request.CurrencyId), request.Amount);
            await sender.Send(command);

            return Results.Ok();
        });
    }
}