using AccommodationBooking.Domain.Users.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Users.Mappers;

[Mapper]

public partial class UserMapper
{
    public partial User ToDomain(Models.User user);

    public partial Models.User ToInfrastructure(User user);

    public partial User ToDomain(Models.User user, string Role);
}