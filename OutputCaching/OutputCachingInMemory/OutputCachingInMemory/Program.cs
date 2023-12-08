using Dapper;
using OutputCachingInMemory.Extensions;
using OutputCachingInMemory.Models;
using OutputCachingInMemory.Repositories;
using OutputCachingInMemory.SqlTypeHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/customers", async (ICustomerRepository customerRepository) =>
{
    var customers = await customerRepository.GetAllAsync();
    return Results.Ok(customers);
});

app.MapGet("/customers/{id:guid}", async (Guid id, ICustomerRepository customerRepository) =>
{
    var customer = await customerRepository.GetAsync(id);
    return customer is null ? Results.NotFound() : Results.Ok(customer);
});

app.MapPost("/customers", async (Customer customer, ICustomerRepository customerRepository) =>
{
    await customerRepository.CreateAsync(customer);
    Results.Created($"customers/{customer.Id}", customer);
});

app.MapPut("/customers/{id:guid}", async (Guid id, Customer customer, ICustomerRepository customerRepository) =>
{
    customer.Id = id;
    await customerRepository.UpdateAsync(customer);
    return Results.Ok(customer);
});

app.MapDelete("/customers/{id:guid}", async (Guid id, ICustomerRepository customerRepository) =>
{
    await customerRepository.DeleteAsync(id);
    Results.Ok(id);
});

app.Run();