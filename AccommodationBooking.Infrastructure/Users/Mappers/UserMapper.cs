using AccommodationBooking.Infrastructure.Users.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Users.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial Domain.Users.Models.User ToDomainUser(User user);
    public partial User ToInfrastructureUser(Domain.Users.Models.User user);
}