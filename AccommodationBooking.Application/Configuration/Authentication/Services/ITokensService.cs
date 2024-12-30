namespace AccommodationBooking.Application.Configuration.Authentication.Services;

public interface ITokensService
{
    string GenerateToken(Infrastructure.Users.Models.User user);
}