using AccommodationBooking.Domain.Users.Models;

namespace AccommodationBooking.Domain.Users.Services;

public interface IUserService
{
    Task<Models.User> Login(LoginRequest loginDTO);
    //Task Logout(string token);
    Task<Models.User> Register(Models.User model);
    Task<List<Models.User>> GetUsers(int page, int pageSize);
}
