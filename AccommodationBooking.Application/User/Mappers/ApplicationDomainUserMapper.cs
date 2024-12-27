using AccommodationBooking.Application.User.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Application.User.Mappers;

[Mapper]
public partial class ApplicationDomainUserMapper
{
    public partial Domain.Users.Models.LoginRequest ToDomainLoginRequest(LoginRequest loginRequest);
}
