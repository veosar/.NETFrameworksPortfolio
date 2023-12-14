using System.Net;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using S3Api.Models.Options;
using S3Api.Services;
using S3Api.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAmazonS3, AmazonS3Client>();
builder.Services.Configure<AWSOptions>(
    builder.Configuration.GetSection(AWSOptions.SectionName));
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IImageFileValidator, ImageFileValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/images/{id:guid}", async (Guid id, IImageService imageService) =>
{
    var response = await imageService.GetImageAsync(id);
    return response is null ? Results.NotFound() : Results.File(response.ResponseStream, response.Headers.ContentType);
});

app.MapGet("/images-blurred/{id:guid}", async (Guid id, IImageService imageService) =>
{
    var response = await imageService.GetImageBlurredAsync(id);
    return response is null ? Results.NotFound() : Results.File(response.ResponseStream, response.Headers.ContentType);
});

app.MapPost("/images/{id:guid}", async (Guid id, [FromForm] IFormFile file, IImageService imageService, IImageFileValidator validator) =>
{
    var isValidImage = validator.IsValidImageFile(file);
    if (!isValidImage)
    {
        return Results.BadRequest("File sent is not a valid image");
    }
    var response = await imageService.UploadImageAsync(id, file);
    return response.HttpStatusCode == HttpStatusCode.OK ? Results.Ok() : Results.BadRequest();
}).DisableAntiforgery();

app.MapDelete("/images/{id:guid}", async (Guid id, IImageService imageService) =>
{
    var response = await imageService.DeleteImageAsync(id);
    return response.HttpStatusCode == HttpStatusCode.NoContent ? Results.Ok() : Results.BadRequest();
});

app.Run();