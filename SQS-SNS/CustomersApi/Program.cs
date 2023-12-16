using System.Data;
using Amazon.SimpleNotificationService;
using CustomersApi.Messaging;
using CustomersApi.Models;
using CustomersApi.Models.Options;
using CustomersApi.Repositories;
using CustomersApi.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SNSOptions>(builder.Configuration.GetSection(SNSOptions.Section));
builder.Services.AddSingleton<IAmazonSimpleNotificationService, AmazonSimpleNotificationServiceClient>();
builder.Services.AddSingleton<ISnsMessenger, SnsMessenger>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(builder.Configuration.GetConnectionString("PostgresSql")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/customers", async (ICustomerService customerService) =>
{
    var customers = await customerService.GetAllAsync();
    return Results.Ok(customers);
}).CacheOutput(x => x.Cache().Tag("customers"));

app.MapGet("/customers/{id:guid}", async (Guid id, ICustomerService customerService) =>
{
    var customer = await customerService.GetAsync(id);
    return customer is null ? Results.NotFound() : Results.Ok(customer);
}).CacheOutput(x => x.Cache().Tag("customers"));

app.MapPost("/customers", async (Customer customer, ICustomerService customerService) =>
{
    await customerService.CreateAsync(customer);
    Results.Created($"customers/{customer.Id}", customer);
}).CacheOutput(x => x.NoCache());

app.MapPut("/customers/{id:guid}", async (Guid id, Customer customer, ICustomerService customerService) =>
{
    customer.Id = id;
    var customerUpdated = await customerService.UpdateAsync(customer);
    return customerUpdated is not null ? Results.Ok(customer) : Results.NotFound();
}).CacheOutput(x => x.NoCache());

app.MapDelete("/customers/{id:guid}", async (Guid id, ICustomerService customerService) =>
{
    await customerService.DeleteAsync(id);
    Results.Ok(id);
}).CacheOutput(x => x.NoCache());

app.Run();