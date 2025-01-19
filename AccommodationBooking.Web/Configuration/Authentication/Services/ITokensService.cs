using AccommodationBooking.Web.User.Models;

namespace AccommodationBooking.Web.Configuration.Authentication.Services;

public interface ITokensService
{
    string GenerateToken(User.Models.User user);
}