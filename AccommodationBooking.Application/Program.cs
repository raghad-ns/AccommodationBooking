using AccommodationBooking.Application.Configuration.Database.Extensions;
using AccommodationBooking.Application.Configuration.Database.Models;
using AccommodationBooking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AccommodationBooking.Application.Configuration.Authentication.Extensions;
using AccommodationBooking.Application.Dependencies;
using AccommodationBooking.Application.Middlewares.ExceptionsHandler;

var builder = WebApplication.CreateBuilder(args);

// Add authentication services to the container.
builder.Services
    .LoadDatabaseConfiguration(builder)
    .LoadAuthenticationConfiguration(builder);

var serviceProvider = builder.Services.BuildServiceProvider();

builder.Services
    .AddDatabaseConfiguration(serviceProvider)
    .ConfigureAuthentication(serviceProvider);

// Register dependencies
builder.Services
    .RegisterAuthenticationDependencies()
    .RegisterMappers()
    .RegisterServices()
    .RegisterRepositories()
    .RegisterValidators();

builder.Services.AddScoped<RequestDelegate>();

builder.Services.AddControllers();

//builder.Services.AddSwaggerGen();
//app.UseSwagger();
//app.UseSwaggerUI();
var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseMiddleware<ExceptionsHandler>();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();