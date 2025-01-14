using System.ComponentModel.DataAnnotations;

namespace AccommodationBooking.Application.User.Models;

public record LoginRequest(string UserName, string Password);
