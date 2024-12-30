using AccommodationBooking.Domain.User.Repositories;
using AccommodationBooking.Domain.User.Validators;
using AccommodationBooking.Domain.Users.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<Users.Models.User> _userValidator;
        private readonly IValidator<LoginRequest> _loginValidator;
        public UserService(
            IUserRepository userRepository,
            IValidator<Users.Models.User> userValidator,
            IValidator<LoginRequest> loginValidator
            )
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _loginValidator = loginValidator;
        }

        public Task<List<Users.Models.User>> GetUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public Task<Users.Models.User> Login(LoginRequest login)
        {
            if (!_loginValidator.Validate(login).IsValid)
            {
                return null;
            }
            return _userRepository.Login(login);
        }

        // public Task Logout(string token)
        // {
        //     throw new NotImplementedException();
        // }

        public Task<Users.Models.User> Register(Users.Models.User model)
        {
            if (!_userValidator.Validate(model).IsValid)
            {
                return null;
            }
            return _userRepository.Register(model);
        }
    }
}
