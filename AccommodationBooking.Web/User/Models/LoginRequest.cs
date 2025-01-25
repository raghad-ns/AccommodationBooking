using System.ComponentModel.DataAnnotations;

namespace AccommodationBooking.Web.User.Models;

public record LoginRequest(string UserName, string Password);
