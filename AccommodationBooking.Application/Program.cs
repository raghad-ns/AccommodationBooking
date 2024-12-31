using AccommodationBooking.Application.Configuration.Database.Extensions;
using AccommodationBooking.Application.Configuration.Database.Models;
using AccommodationBooking.Domain.User.Repositories;
using AccommodationBooking.Domain.User.Services;
using AccommodationBooking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using AccommodationBooking.Application.Configuration.Authentication.Extensions;
using AccommodationBooking.Application.Configuration.Authentication.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using AccommodationBooking.Domain.User.Validators;
using AccommodationBooking.Infrastructure.Users.Repositories;
using AccommodationBooking.Domain.Users.Models;
using AccommodationBooking.Infrastructure.Users.Mappers;
using AccommodationBooking.Application.User.Mappers;
using Microsoft.AspNetCore.Identity;
using AccommodationBooking.Application.Configuration.Authentication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add authentication services to the container.
builder.Services
    .LoadDatabaseConfiguration(builder)
    .LoadAuthenticationConfiguration(builder);


// Add DbContext to the container
builder.Services.AddDbContext<AccommodationBookingContext>(options =>
{
    var serviceProvider = builder.Services.BuildServiceProvider();
    var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
    options.UseSqlServer(databaseOptions.ConnectionString);
});

builder.Services
    .AddIdentity<AccommodationBooking.Infrastructure.Users.Models.User, IdentityRole>(options =>
    {
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;

    })
    .AddEntityFrameworkStores<AccommodationBookingContext>();

var authenticationConfiguration = builder
    .Services
    .BuildServiceProvider()
    .GetRequiredService<IOptions<AuthenticationOptions>>()
    .Value;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(p =>
{
    var key = Encoding.UTF8.GetBytes(authenticationConfiguration.Key);
    p.SaveToken = true;
    p.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = authenticationConfiguration.Issuer ,
        ValidateAudience = true,
        ValidAudience = authenticationConfiguration.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Add services
builder.Services.AddScoped<AuthenticationOptions>();
builder.Services.AddScoped<DatabaseOptions>();
builder.Services.AddScoped<AccommodationBookingContext>();
builder.Services.AddScoped<ITokensService, TokensService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
builder.Services.AddScoped<UserManager<AccommodationBooking.Infrastructure.Users.Models.User>>();
builder.Services.AddScoped<SignInManager<AccommodationBooking.Infrastructure.Users.Models.User>>();

// Add mappers
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<ApplicationDomainUserMapper>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();

