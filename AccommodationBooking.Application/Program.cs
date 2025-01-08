using AccommodationBooking.Application.Configuration.Database.Extensions;
using AccommodationBooking.Application.Configuration.Database.Models;
using AccommodationBooking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AccommodationBooking.Application.Configuration.Authentication.Extensions;
using AccommodationBooking.Application.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add authentication services to the container.
builder.Services
    .LoadDatabaseConfiguration(builder)
    .LoadAuthenticationConfiguration(builder);


var serviceProvider = builder.Services.BuildServiceProvider();
// Add DbContext to the container
builder.Services.AddDbContext<AccommodationBookingContext>(options =>
{
    var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
    options.UseSqlServer(databaseOptions.ConnectionString);
});

builder.Services
    //.AddDatabaseConfiguration(serviceProvider)
    .ConfigureAuthentication(serviceProvider);

// Register dependencies
builder.Services
    .RegisterAuthenticationDependencies()
    .RegisterMappers()
    .RegisterRepositories()
    .RegisterValidators()
    .RegisterServices();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//using var scope = serviceProvider.CreateScope();
//var context = scope.ServiceProvider.GetRequiredService<AccommodationBookingContext>();
//Console.WriteLine(context != null ? "DbContext registered successfully" : "DbContext registration failed");


app.MapGet("/", () => "Hello World!");

app.Run();