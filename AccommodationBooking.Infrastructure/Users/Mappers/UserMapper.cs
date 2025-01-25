using AccommodationBooking.Domain.Users.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Users.Mappers;

[Mapper]

public static partial class UserMapper
{
    public static partial User ToDomain(this Models.User user);

    public static partial Models.User ToInfrastructure(this User user);

    public static partial User ToDomain(this Models.User user, string Role);
}