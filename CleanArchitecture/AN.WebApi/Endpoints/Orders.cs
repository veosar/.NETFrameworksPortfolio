using AN.Application.Orders.AddLineItem;
using AN.Application.Orders.Create;
using AN.Application.Orders.GetOrder;
using AN.Application.Orders.GetOrderSummary;
using AN.Application.Orders.RemoveLineItem;
using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Orders;
using AN.Domain.Entities.Products;
using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Orders;
using AN.Domain.Exceptions.Entities.Products;
using AN.WebApi.Common;
using AN.WebApi.Contracts.Orders;
using Carter;
using MediatR;

namespace AN.WebApi.Endpoints;

public class Orders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (Guid customerId, ISender sender) =>
        {
            var command = new CreateOrderCommand(CustomerId.Create(customerId));
            var orderId = await sender.Send(command);
            return Results.Created($"orders/{orderId}/summary", orderId);
        });

        app.MapPut("orders/{id:guid}/line-items", async (Guid id, AddLineItemRequest request, ISender sender) =>
        {
            try
            {
                var command = new AddLineItemCommand(OrderId.Create(id), ProductId.Create(request.ProductId),
                    CurrencyId.Create(request.CurrencyId), request.Amount);
                await sender.Send(command);
                return Results.Ok();
            }
            catch (OrderNotFoundDomainException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
        
        app.MapDelete("orders/{id:guid}/line-items/{lineItemId:guid}", async (Guid id, Guid lineItemId, ISender sender) =>
        {
            try
            {
                var command = new RemoveLineItemCommand(OrderId.Create(id), LineItemId.Create(lineItemId));
                await sender.Send(command);
                return Results.Ok();
            }
            catch (OrderNotFoundDomainException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (LineItemNotFoundDomainException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (HasOneLineItemDomainException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        
        app.MapGet("orders/{id}", async (Guid id, ISender sender) =>
        {
            try
            {
                var query = new GetOrderQuery(OrderId.Create(id));
                var result = await sender.Send(query);
                return Results.Ok(result);
            }
            catch (OrderNotFoundDomainException ex)
            {
                return Results.NotFound(ex.Message);
            }
            
        });

        app.MapGet("orders/{id}/summary", async (Guid id, ISender sender) =>
        {
            try
            {
                var query = new GetOrderSummaryQuery(OrderId.Create(id));
                var result = await sender.Send(query);
                return Results.Ok(result);
            }
            catch (OrderNotFoundDomainException ex)
            {
                return Results.NotFound(ex.Message);
            }
        }).RequireRateLimiting(Constants.FixedRateLimiterPolicy);
    }
}