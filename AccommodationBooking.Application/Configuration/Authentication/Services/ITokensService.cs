using AccommodationBooking.Application.User.Models;

namespace AccommodationBooking.Application.Configuration.Authentication.Services;

public interface ITokensService
{
    string GenerateToken(User.Models.User user);
}