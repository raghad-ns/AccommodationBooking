using AccommodationBooking.Application.Configuration.Authentication.Models;
using AccommodationBooking.Domain.Cities.Repositories;
using AccommodationBooking.Domain.Cities.Services;
using AccommodationBooking.Domain.Hotels.Repositories;
using AccommodationBooking.Domain.Hotels.Services;
using AccommodationBooking.Domain.Users.Repositories;
using AccommodationBooking.Domain.Users.Services;
using AccommodationBooking.Domain.Users.Validators;
using AccommodationBooking.Infrastructure.Cities.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Hotels.Repositories;
using AccommodationBooking.Infrastructure.Users.Mappers;
using AccommodationBooking.Infrastructure.Users.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using AccommodationBooking.Domain.Rooms.Services;
using AccommodationBooking.Domain.Rooms.Repositories;
using AccommodationBooking.Infrastructure.Rooms.Repositories;
using AccommodationBooking.Web.Configuration.Authentication.Services;
using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Cities.Validators;
using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Domain.Hotels.Validators;
using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Domain.Rooms.Validators;

namespace AccommodationBooking.Framework.Dependencies;

public static class DependenciesRegistration
{
    public static IServiceCollection RegisterAuthenticationDependencies(this IServiceCollection services)
    {
        services
            .AddScoped<AuthenticationOptions>()
            .AddScoped<AccommodationBookingContext>()
            .AddScoped<UserManager<Infrastructure.Users.Models.User>>()
            .AddScoped<SignInManager<Infrastructure.Users.Models.User>>()
            .AddScoped<ITokensService, TokensService>()
            .AddScoped<IUserService, UserService>();

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddScoped<ITokensService, TokensService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICityService, CityService>()
            .AddScoped<IHotelService, HotelService>()
            .AddScoped<IRoomService, RoomService>();

        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IHotelRepository, HotelRepository>()
            .AddScoped<ICityRepository, CityRepository>()
            .AddScoped<IRoomRepository, RoomRepository>();

        return services;
    }

    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<Domain.Users.Models.User>, UserValidator>()
            .AddScoped<IValidator<Domain.Users.Models.LoginRequest>, LoginValidator>()
            .AddScoped <IValidator <City>, CityValidator>()
            .AddScoped<IValidator<Hotel>, HotelValidator>()
            .AddScoped<IValidator<Room>, RoomValidator>();

        return services;
    }
}