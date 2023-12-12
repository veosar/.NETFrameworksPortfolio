using BogusCommon.Models.Options;
using BogusCommon.Services;
using BogusDBApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<FakeDataOptions>(
    builder.Configuration.GetSection(FakeDataOptions.SectionName));

builder.Services.AddTransient<IFakeDataGeneratorService, FakeDataGeneratorService>();

builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSql"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Note: This is not how this should be normally done. Applying migrations should probably be done by CI/CD, but it's for showcase example so it's reproducible in Docker.
    var dbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/orders", async (OrdersDbContext context) =>
{
    var orders = await context.Orders
        .Include(o => o.Customer).ThenInclude(c => c.Address)
        .Include(o => o.Items)
        .ToListAsync();

    return Results.Ok(orders);
});

app.MapPost("/seeddatabase/{amount:int}", async (int amount, OrdersDbContext context, IFakeDataGeneratorService fakeDataGenerator) =>
{
    if (amount < 0 || amount > 100000)
    {
        return Results.BadRequest($"Amount has to be set between 0 and 100 000.");
    }
    
    var orders = fakeDataGenerator.GetFakeOrders().Take(amount).ToList();
    context.Orders.AddRange(orders);
    context.SaveChanges();

    return Results.Created();
});

app.Run();