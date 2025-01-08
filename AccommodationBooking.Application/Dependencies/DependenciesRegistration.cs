using AccommodationBooking.Application.Configuration.Authentication.Models;
using AccommodationBooking.Application.Configuration.Authentication.Services;
using AccommodationBooking.Application.User.Mappers;
using AccommodationBooking.Domain.Users.Repositories;
using AccommodationBooking.Domain.Users.Services;
using AccommodationBooking.Domain.Users.Validators;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Users.Mappers;
using AccommodationBooking.Infrastructure.Users.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AccommodationBooking.Application.Dependencies;

public static class DependenciesRegistration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddScoped<ITokensService, TokensService>()
            .AddScoped<IUserService, UserService>();

        return services;
    }

    public static IServiceCollection RegisterAuthenticationDependencies(this IServiceCollection services)
    {
        services
            .AddScoped<AuthenticationOptions>()
            .AddScoped<AccommodationBookingContext>()
            .AddScoped<SymmetricSecurityKey>()
            .AddScoped<UserManager<Infrastructure.Users.Models.User>>()
            .AddScoped<SignInManager<Infrastructure.Users.Models.User>>();

        return services;
    }

    public static IServiceCollection RegisterMappers(this IServiceCollection services)
    {
        services
            .AddScoped<UserMapper>()
            .AddScoped<ApplicationDomainUserMapper>();

        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<Domain.Users.Models.User>, UserValidator>()
            .AddScoped<IValidator<Domain.Users.Models.LoginRequest>, LoginValidator>();

        return services;
    }
}
