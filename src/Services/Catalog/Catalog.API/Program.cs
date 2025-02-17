// builder is an instance of WebApplicationBuilder created by the WebApplication.CreateBuilder method.
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

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
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// FluentValidation is a library that provides a fluent interface for validating objects.
builder.Services.AddValidatorsFromAssembly(assembly);

// Marten is a library that provides a document database for .NET applications.
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

// Add a global exception handler.
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Add health checks for application and the database.
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

// app is an instance of WebApplication created by the WebApplicationBuilder.
var app = builder.Build();

// Configure the HTTP request pipeline.
// Mapster is a library that provides a simple way to map objects.
app.MapCarter();

// Global handlers for unhandled exceptions.
app.UseExceptionHandler(options => { });

// Add health checks.
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

// Run the application.
app.Run();
