using AccommodationBooking.Application.Configuration.Database.Models;
using AccommodationBooking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IServiceProvider serviceProvider)
    {
        services.AddDbContext<AccommodationBookingContext>(options =>
        {
            var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseSqlServer(databaseOptions.ConnectionString);
        });

        return services;
    }
}