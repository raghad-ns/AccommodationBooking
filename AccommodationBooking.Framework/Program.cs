using AccommodationBooking.Application.Configuration.Database.Extensions;
using AccommodationBooking.Application.Configuration.Authentication.Extensions;
using AccommodationBooking.Framework.Dependencies;
using AccommodationBooking.Web.Middlewares.ExceptionsHandler;
using AccommodationBooking.Application.User.Controllers;
using AccommodationBooking.Web.Cities.Controllers;
using AccommodationBooking.Web.Hotels.Controllers;
using AccommodationBooking.Web.Rooms.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add authentication services to the container.
builder.Services
    .LoadDatabaseConfiguration(builder)
    .LoadAuthenticationConfiguration(builder);

builder.Services
    .AddDatabaseConfiguration()
    .ConfigureAuthentication()
    .ConfigureAuthorization();

// Register dependencies
builder.Services
    .RegisterAuthenticationDependencies()
    .RegisterServices()
    .RegisterRepositories()
    .RegisterValidators();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(UserController).Assembly)
    .AddApplicationPart(typeof(CitiesController).Assembly)
    .AddApplicationPart(typeof(HotelsController).Assembly)
    .AddApplicationPart(typeof(RoomsController).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapSwagger().RequireAuthorization();

app.UseDeveloperExceptionPage();
app.UseMiddleware<ExceptionsHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();