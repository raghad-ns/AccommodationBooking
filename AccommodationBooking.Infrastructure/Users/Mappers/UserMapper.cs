using AccommodationBooking.Domain.Users.Models;
using Microsoft.AspNetCore.Identity;
using Riok.Mapperly.Abstractions;
using System.Data;

namespace AccommodationBooking.Infrastructure.Users.Mappers;

[Mapper]

public static partial class UserMapper
{
    public static partial User ToDomain(this Models.User user);

    public static partial Models.User ToInfrastructure(this User user);

    public static User ToDomain(this Models.User user, string role)
    {
        var domainUser = ToDomain(user);

        if (!string.IsNullOrEmpty(role) && Enum.TryParse<Role>(role, ignoreCase: true, out var domainRole))
        {
            domainUser.Role = domainRole;
        }

        domainUser.Role = Role.User; // Default role for unrecognized or missing roles
        return domainUser;
    }
}