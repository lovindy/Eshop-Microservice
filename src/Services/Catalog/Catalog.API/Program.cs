// builder is an instance of WebApplicationBuilder created by the WebApplication.CreateBuilder method.
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();

// assembly is an instance of Assembly that represents the current assembly.
var assembly = typeof(Program).Assembly;

// MediatR is a library that simplifies the implementation of the mediator pattern.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// FluentValidation is a library that provides a fluent interface for validating objects.
builder.Services.AddValidatorsFromAssembly(assembly);

// Marten is a library that provides a document database for .NET applications.
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

// app is an instance of WebApplication created by the WebApplicationBuilder.
var app = builder.Build();

// Configure the HTTP request pipeline.
// Mapster is a library that provides a simple way to map objects.
app.MapCarter();

// Global handlers for unhandled exceptions.
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = Application.Json;

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        var isDevelopment = builder.Environment.IsDevelopment();
        var errorDetails = new
        {
            Error = "An unexpected error occurred.",
            Path = exceptionHandlerPathFeature?.Path,
            ExceptionMessage = isDevelopment ? exceptionHandlerPathFeature?.Error.Message : null,
            StackTrace = isDevelopment ? exceptionHandlerPathFeature?.Error.StackTrace : null
        };

        var errorJson = System.Text.Json.JsonSerializer.Serialize(errorDetails);
        await context.Response.WriteAsync(errorJson);
    });
});

app.Run();
