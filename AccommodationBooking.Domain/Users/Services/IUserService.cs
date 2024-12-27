using AccommodationBooking.Domain.Users.Models;

namespace AccommodationBooking.Domain.User.Services;

public interface IUserService
{
    Task<Users.Models.User> Login(LoginRequest loginDTO);
    //Task Logout(string token);
    Task<Users.Models.User> Register(Users.Models.User model);
    Task<List<Users.Models.User>> GetUsers();
}
