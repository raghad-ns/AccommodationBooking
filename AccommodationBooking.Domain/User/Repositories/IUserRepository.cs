using AccommodationBooking.Domain.User.Models;

namespace AccommodationBooking.Domain.User.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> Login(LoginDTO loginDTO);
        //Task Logout(string token);
        Task<UserModel> Register(UserModel model);
        Task<List<UserModel>> GetAllUsers();
    }
}
