using AN.Application.Currencies.Create;
using AN.Application.Currencies.Delete;
using AN.Application.Currencies.Get;
using AN.Application.Currencies.GetAll;
using AN.Application.Currencies.Update;
using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Shared;
using AN.WebApi.Contracts.Currencies;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AN.WebApi.Endpoints;

public class Currencies : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("currencies", async (ISender sender) =>
        {
            var command = new GetAllCurrenciesQuery();
            var currencies = await sender.Send(command);
            return Results.Ok(currencies);
        });
        
        app.MapGet("currencies/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var command = new GetCurrencyQuery(CurrencyId.Create(id));
                var currency = await sender.Send(command);
                return Results.Ok(currency);
            }
            catch (CurrencyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
        
        app.MapPost("currencies", async (CreateCurrencyRequest request, ISender sender) =>
        {
            var command = new CreateCurrencyCommand(CurrencyName.Create(request.Name));
            var currencyId = await sender.Send(command);
            return Results.Created($"currencies/{currencyId}", currencyId);
        });
        
        app.MapDelete("currencies/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var command = new DeleteCurrencyCommand(CurrencyId.Create(id));
                await sender.Send(command);
                return Results.NoContent();
            }
            catch (CurrencyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
        
        app.MapPut("currencies/{id:guid}", async (Guid id, [FromBody] UpdateCurrencyRequest request, ISender sender) =>
        {
            try
            {
                var command = new UpdateCurrencyCommand(CurrencyId.Create(id), CurrencyName.Create(request.Name));
                await sender.Send(command);
                return Results.Ok();
            }
            catch (CurrencyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
    }
}