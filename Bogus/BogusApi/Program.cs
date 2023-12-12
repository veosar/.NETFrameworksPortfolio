using Bogus;
using Bogus.Models.Options;
using Bogus.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<FakeDataOptions>(
    builder.Configuration.GetSection(FakeDataOptions.SectionName));

builder.Services.AddSingleton<IFakeDataGeneratorService, FakeDataGeneratorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/fake/customer", (IFakeDataGeneratorService fakeDataGeneratorService) =>
{
    var customer = fakeDataGeneratorService.GetFakeCustomer();
    return Results.Ok(customer);
});

app.MapGet("/fake/customer/{amount:int}", (int amount, IFakeDataGeneratorService fakeDataGeneratorService) =>
{
    if (amount > 10000 || amount < 0)
    {
        return Results.BadRequest("The amount of fake data specified must be between 0 and 10000");
    }
    
    var customers = fakeDataGeneratorService.GetFakeCustomers().Take(amount);
    return Results.Ok(customers);
});

app.MapGet("/fake/item", (IFakeDataGeneratorService fakeDataGeneratorService) =>
{
    var item = fakeDataGeneratorService.GetFakeItem();
    return Results.Ok(item);
});

app.MapGet("/fake/item/{amount:int}", (int amount, IFakeDataGeneratorService fakeDataGeneratorService) =>
{
    if (amount > 10000 || amount < 0)
    {
        return Results.BadRequest("The amount of fake data specified must be between 0 and 10000");
    }
    
    var items = fakeDataGeneratorService.GetFakeItems().Take(amount);
    return Results.Ok(items);
});

app.MapGet("/fake/address", (IFakeDataGeneratorService fakeDataGeneratorService) =>
{
    var address = fakeDataGeneratorService.GetFakeAddress();
    return Results.Ok(address);
});

app.MapGet("/fake/address/{amount:int}", (int amount, IFakeDataGeneratorService fakeDataGeneratorService) =>
{
    if (amount > 10000 || amount < 0)
    {
        return Results.BadRequest("The amount of fake data specified must be between 0 and 10000");
    }
    
    var addresses = fakeDataGeneratorService.GetFakeAddresses().Take(amount);
    return Results.Ok(addresses);
});

app.MapGet("/fake/order", (IFakeDataGeneratorService fakeDataGeneratorService) =>
{
    var order = fakeDataGeneratorService.GetFakeOrder();
    return Results.Ok(order);
});

app.MapGet("/fake/order/{amount:int}", (int amount, IFakeDataGeneratorService fakeDataGeneratorService) =>
{
    if (amount > 10000 || amount < 0)
    {
        return Results.BadRequest("The amount of fake data specified must be between 0 and 10000");
    }
    
    var orders = fakeDataGeneratorService.GetFakeOrders().Take(amount);
    return Results.Ok(orders);
});

app.Run();