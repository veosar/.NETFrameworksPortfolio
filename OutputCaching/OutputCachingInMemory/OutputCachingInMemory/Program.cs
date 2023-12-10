using Dapper;
using Microsoft.AspNetCore.OutputCaching;
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
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(x => x.NoCache()); //NoCache by default so it does not mess up with POST/PUT/DELETE
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseOutputCache();

app.MapGet("/customers", async (ICustomerRepository customerRepository) =>
{
    var customers = await customerRepository.GetAllAsync();
    return Results.Ok(customers);
}).CacheOutput(x => x.Cache().Tag("customers"));

app.MapGet("/customers/{id:guid}", async (Guid id, ICustomerRepository customerRepository) =>
{
    var customer = await customerRepository.GetAsync(id);
    return customer is null ? Results.NotFound() : Results.Ok(customer);
}).CacheOutput(x => x.Cache().Tag("customers"));

app.MapPost("/customers", async (Customer customer, ICustomerRepository customerRepository, IOutputCacheStore outputCacheStore,
    CancellationToken cancellationToken) =>
{
    await customerRepository.CreateAsync(customer);
    await outputCacheStore.EvictByTagAsync("customers", cancellationToken);
    Results.Created($"customers/{customer.Id}", customer);
});

app.MapPut("/customers/{id:guid}", async (Guid id, Customer customer, ICustomerRepository customerRepository, IOutputCacheStore outputCacheStore,
    CancellationToken cancellationToken) =>
{
    customer.Id = id;
    await customerRepository.UpdateAsync(customer);
    await outputCacheStore.EvictByTagAsync("customers", cancellationToken);
    return Results.Ok(customer);
});

app.MapDelete("/customers/{id:guid}", async (Guid id, ICustomerRepository customerRepository, IOutputCacheStore outputCacheStore,
    CancellationToken cancellationToken) =>
{
    await customerRepository.DeleteAsync(id);
    await outputCacheStore.EvictByTagAsync("customers", cancellationToken);
    Results.Ok(id);
});

app.MapGet("/customersnocache", async (ICustomerRepository customerRepository) =>
{
    var customers = await customerRepository.GetAllAsync();
    return Results.Ok(customers);
});

app.MapGet("/customersnocache/{id:guid}", async (Guid id, ICustomerRepository customerRepository) =>
{
    var customer = await customerRepository.GetAsync(id);
    return customer is null ? Results.NotFound() : Results.Ok(customer);
});

app.Run();