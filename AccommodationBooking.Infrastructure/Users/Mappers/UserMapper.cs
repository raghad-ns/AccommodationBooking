using AccommodationBooking.Domain.Users.Models;
using Microsoft.AspNetCore.Identity;
using Riok.Mapperly.Abstractions;
using System.Data;

namespace AccommodationBooking.Infrastructure.Users.Mappers;

[Mapper]

public partial class UserMapper
{
    public partial User ToDomain(Models.User user);

    public partial Models.User ToInfrastructure(User user);

    public User ToDomain(Models.User user, string role)
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