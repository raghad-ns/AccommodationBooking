using System.ComponentModel.DataAnnotations;

namespace AccommodationBooking.Application.User.Models;

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
