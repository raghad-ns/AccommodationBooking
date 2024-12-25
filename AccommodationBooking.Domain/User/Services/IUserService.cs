using AccommodationBooking.Domain.User.Models;

namespace AccommodationBooking.Domain.User.Services;

public interface IUserService
{
    Task<UserModel> Login(string  username, string password);
    Task Logout(string token);
    Task<UserModel> Register(UserModel model);
    Task<List<UserModel>> GetUsers();
}
