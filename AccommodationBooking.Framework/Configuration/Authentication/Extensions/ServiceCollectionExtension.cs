using AccommodationBooking.Application.Configuration.Authentication.Models;
using AccommodationBooking.Application.Configuration.Database.Models;
using AccommodationBooking.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        services
            .AddIdentity<Infrastructure.Users.Models.User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;

            })
            .AddEntityFrameworkStores<AccommodationBookingContext>();

        var authenticationConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<AuthenticationOptions>>().Value;

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(p =>
            {
                var key = Encoding.UTF8.GetBytes(authenticationConfiguration.Key);
                p.SaveToken = true;
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authenticationConfiguration.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authenticationConfiguration.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        return services;
    }

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));

            options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
        });
        return services;
    }
}