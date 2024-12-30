using AccommodationBooking.Infrastructure.Users.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Users.Mappers;

[Mapper]
public partial class UserMapper
{
    public Domain.Users.Models.User ToDomainUser(User user) =>
        new Domain.Users.Models.User
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.PasswordHash,
            FirstName = user.NormalizedUserName,
            PhoneNumber = user.PhoneNumber
        };

    [UserMapping]
    public User ToInfrastructureUser(Domain.Users.Models.User user) => 
     new User
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            NormalizedUserName = user.FirstName + " " + user.LastName,
            PasswordHash = user.Password,
            PhoneNumber = user?.PhoneNumber,
        };
}