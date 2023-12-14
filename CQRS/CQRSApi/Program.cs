using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using CQRSApi.Models.Commands;
using CQRSApi.Models.Queries;
using CQRSApi.Models.Requests;
using CQRSApi.Repositories.ReadRepositories;
using CQRSApi.Repositories.WriteRepositories;
using CQRSApi.SqlTypeHandlers;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
builder.Services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(builder.Configuration.GetConnectionString("PostgresSql")));
builder.Services.AddScoped<IMovieReadRepository, MovieReadRepository>();
builder.Services.AddScoped<IMovieWriteRepository, MovieWriteRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/movies", async (IMediator mediator) =>
{
    var getAllMoviesQuery = new GetAllMoviesQuery();

    var result = await mediator.Send(getAllMoviesQuery);

    return result;
});

app.MapGet("/movies/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var getMovieByIdQuery = new GetMovieByIdQuery
    {
        Id = id    
    };

    var result = await mediator.Send(getMovieByIdQuery);

    return result is null ? Results.NotFound() : Results.Ok(result);
}).WithName("GetMovieById");;

app.MapPost("/movies", async ([FromBody] CreateMovieRequest createMovieRequest, IMediator mediator) =>
{
    var command = new CreateMovieCommand
    {
        Title = createMovieRequest.Title,
        Genre = createMovieRequest.Genre,
        DateOfRelease = createMovieRequest.DateOfRelease
    };

    var result = await mediator.Send(command);

    return Results.CreatedAtRoute("GetMovieById", new {id = result});
});

app.MapPut("/movies", async (UpdateMovieRequest updateMovieRequest, IMediator mediator) =>
{
    var command = new UpdateMovieCommand
    {
        Id = updateMovieRequest.Id,
        Title = updateMovieRequest.Title,
        Genre = updateMovieRequest.Genre,
        DateOfRelease = updateMovieRequest.DateOfRelease
    };

    await mediator.Send(command);

    return Results.Ok();
});

app.MapDelete("/movies/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var deleteMovieCommand = new DeleteMovieCommand
    {
        Id = id
    };

    await mediator.Send(deleteMovieCommand);

    return Results.Ok();
});

app.Run();