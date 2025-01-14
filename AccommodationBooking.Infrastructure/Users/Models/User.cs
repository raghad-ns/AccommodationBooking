using AccommodationBooking.Domain.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace AccommodationBooking.Infrastructure.Users.Models;

public class User: IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}