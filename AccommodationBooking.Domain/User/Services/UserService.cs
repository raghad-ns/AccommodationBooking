using AccommodationBooking.Domain.User.Models;
using AccommodationBooking.Domain.User.Repositories;
using AccommodationBooking.Domain.User.Validators;

namespace AccommodationBooking.Domain.User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<UserModel>> GetUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public Task<UserModel> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task Logout(string token)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> Register(UserModel model)
        {
            var validator = new UserValidator();
            if (!validator.Validate(model).IsValid)
            {
                return null;
            }
            return _userRepository.Register(model);
        }
    }
}
