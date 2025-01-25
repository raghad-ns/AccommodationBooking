using AccommodationBooking.Web.User.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Web.User.Mappers;

[Mapper]
public static partial class ApplicationDomainUserMapper
{
    public static partial Domain.Users.Models.LoginRequest ToDomain(this LoginRequest loginRequest);
    public static partial LoginRequest ToApplication(this Domain.Users.Models.LoginRequest loginRequest);
    public static partial Domain.Users.Models.User ToDomain(this Web.User.Models.User user);
    public static partial Web.User.Models.User ToApplication(this Domain.Users.Models.User user);
}