using AccommodationBooking.Application.Configuration.Authentication.Models;
using AccommodationBooking.Application.Configuration.Database.Models;

namespace AccommodationBooking.Application.Configuration.Authentication.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection LoadAuthenticationConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services
            .Configure<AuthenticationOptions>(
                builder.Configuration.GetSection(AuthenticationOptions.Authentication)
            );

        return services;
    }
}