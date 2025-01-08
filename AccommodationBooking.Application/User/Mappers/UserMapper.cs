using AccommodationBooking.Application.User.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Application.User.Mappers;

[Mapper]
public partial class ApplicationDomainUserMapper
{
    public partial Domain.Users.Models.LoginRequest ToDomain(LoginRequest loginRequest);
    public partial LoginRequest ToApplication(Domain.Users.Models.LoginRequest loginRequest);
    public partial Domain.Users.Models.User ToDomain(Models.User user);
    public partial Models.User ToApplication(Domain.Users.Models.User user);
}