// builder is an instance of WebApplicationBuilder created by the WebApplication.CreateBuilder method.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// app is an instance of WebApplication created by the WebApplicationBuilder.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
