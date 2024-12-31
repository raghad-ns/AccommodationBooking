using AccommodationBooking.Domain.Users.Models;
using Microsoft.AspNetCore.Identity;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Users.Mappers;

[Mapper]
public partial class UserMapper
{
    private readonly UserManager<Models.User> _userManager;

    public UserMapper(UserManager<Models.User> userManager) { _userManager = userManager; }
    public Domain.Users.Models.User ToDomainUser(Models.User user, Role role) =>
        new Domain.Users.Models.User
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.PasswordHash,
            FirstName = user.NormalizedUserName,
            PhoneNumber = user.PhoneNumber,
            Role = role,
        };

    [UserMapping]
    public Models.User ToInfrastructureUser(Domain.Users.Models.User user) =>
     new Models.User
     {
         Id = user.Id,
         UserName = user.UserName,
         Email = user.Email,
         NormalizedUserName = user.FirstName + " " + user.LastName,
         PasswordHash = user.Password,
         PhoneNumber = user?.PhoneNumber,
     };

    public async Task<Role> ToDomainRole(Models.User user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var role = roles.FirstOrDefault();

        if (!string.IsNullOrEmpty(role) && Enum.TryParse<Role>(role, ignoreCase: true, out var domainRole))
        {
            return domainRole;
        }

        return Role.User; // Default role for unrecognized or missing roles
    }
}