using AccommodationBooking.Domain.Users.Models;

namespace AccommodationBooking.Application.User.Models;

public class User
{
    //public Guid Id { get; set; } 
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Role Role { get; set; }
}