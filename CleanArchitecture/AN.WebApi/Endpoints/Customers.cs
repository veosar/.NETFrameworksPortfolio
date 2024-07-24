using System.ComponentModel.DataAnnotations;
using AN.Application.Customers.Create;
using AN.Application.Customers.Get;
using AN.Application.Customers.GetAll;
using AN.Application.Customers.Update;
using AN.Domain.Entities.Customers;
using AN.Domain.Entities.Shared;
using AN.Domain.Exceptions.Entities.Customers;
using AN.WebApi.Contracts.Customers;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AN.WebApi.Endpoints;

public class Customers : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("customers", async (ISender sender) =>
        {
            var command = new GetAllCustomersQuery();
            var customers = await sender.Send(command);
            return Results.Ok(customers);
        });
        
        app.MapGet("customers/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var command = new GetCustomerQuery(CustomerId.Create(id));
                var customer = await sender.Send(command);
                return Results.Ok(customer);
            }
            catch (CustomerNotFoundDomainException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
        
        app.MapPost("customers", async (CreateCustomerRequest request, ISender sender) =>
        {
            var command = new CreateCustomerCommand(Email.Create(request.Email), FirstName.Create(request.FirstName), LastName.Create(request.LastName));
            var customerId = await sender.Send(command);
            return Results.Created($"customers/{customerId}", customerId);
        });
        
        app.MapPut("customers/{id:guid}", async (Guid id, [FromBody] UpdateCustomerRequest request, ISender sender) =>
        {
            try
            {
                var command = new UpdateCustomerCommand(CustomerId.Create(id), Email.Create(request.Email),
                    FirstName.Create(request.FirstName), LastName.Create(request.LastName));
                await sender.Send(command);
                return Results.Ok();
            }
            catch (CustomerNotFoundDomainException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
    }
}