using AccommodationBooking.Application.Configuration.Database.Extensions;
using AccommodationBooking.Application.Configuration.Database.Models;
using AccommodationBooking.Domain.User.Repositories;
using AccommodationBooking.Domain.User.Services;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.User.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using AccommodationBooking.Application.Configuration.Authentication.Extensions;
using AccommodationBooking.Application.Configuration.Authentication.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using AccommodationBooking.Domain.User.Models;
using AccommodationBooking.Domain.User.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add authentication services to the container.
builder.Services
    .LoadDatabaseConfiguration(builder)
    .LoadAuthenticationConfiguration(builder);

var authenticationConfiguration = new AuthenticationOptions();
var databaseOptions = 
//builder.Services.AddAuthentication(k =>
//{
//    k.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    k.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(p =>
//{
//    var key = Encoding.UTF8.GetBytes(authenticationConfiguration.Key);
//    p.SaveToken = true;
//    p.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateLifetime = false,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = authenticationConfiguration.Key,
//        ValidAudience = authenticationConfiguration.Audience,
//        IssuerSigningKey = new SymmetricSecurityKey(key)
//    };
//});

// Add DbContext to the container
builder.Services.AddDbContext<AccommodationBookingContext>(options =>
{
    var serviceProvider = builder.Services.BuildServiceProvider();
    var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
    options.UseSqlServer(databaseOptions.ConnectionString);
});

// Add services
builder.Services.AddScoped<AccommodationBookingContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IValidator<UserModel>, UserValidator>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();

