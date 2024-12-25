using AccommodationBooking.Application.Configuration.Database.Models;

namespace AccommodationBooking.Application.Configuration.Database.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection LoadDatabaseConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services
            .Configure<DatabaseOptions>(
                builder.Configuration.GetSection(DatabaseOptions.Database)
            );

        return services;
    }
}