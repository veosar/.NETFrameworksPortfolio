using Dapper;
using Microsoft.AspNetCore.OutputCaching;
using OutputCaching.Enums;
using OutputCaching.Extensions;
using OutputCaching.Models;
using OutputCaching.Models.Options;
using OutputCaching.Repositories;
using OutputCaching.SqlTypeHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
builder.Services.AddServices(builder.Configuration);
CacheSettings cacheSettings = new();
builder.Configuration.GetSection(CacheSettings.SectionName)
    .Bind(cacheSettings);

if (cacheSettings.OutputCacheType == OutputCacheType.Redis)
{
    builder.Services.AddStackExchangeRedisOutputCache(x =>
    {
        x.InstanceName = "OutputCachingApi";
        x.Configuration = "redis:6379";
    });
}
    
builder.Services.AddOutputCache();

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
}).CacheOutput(x => x.NoCache());

app.MapPut("/customers/{id:guid}", async (Guid id, Customer customer, ICustomerRepository customerRepository, IOutputCacheStore outputCacheStore,
    CancellationToken cancellationToken) =>
{
    customer.Id = id;
    await customerRepository.UpdateAsync(customer);
    await outputCacheStore.EvictByTagAsync("customers", cancellationToken);
    return Results.Ok(customer);
}).CacheOutput(x => x.NoCache());

app.MapDelete("/customers/{id:guid}", async (Guid id, ICustomerRepository customerRepository, IOutputCacheStore outputCacheStore,
    CancellationToken cancellationToken) =>
{
    await customerRepository.DeleteAsync(id);
    await outputCacheStore.EvictByTagAsync("customers", cancellationToken);
    Results.Ok(id);
}).CacheOutput(x => x.NoCache());

app.MapGet("/customersnocache", async (ICustomerRepository customerRepository) =>
{
    var customers = await customerRepository.GetAllAsync();
    return Results.Ok(customers);
}).CacheOutput(x => x.NoCache());

app.MapGet("/customersnocache/{id:guid}", async (Guid id, ICustomerRepository customerRepository) =>
{
    var customer = await customerRepository.GetAsync(id);
    return customer is null ? Results.NotFound() : Results.Ok(customer);
}).CacheOutput(x => x.NoCache());

app.Run();