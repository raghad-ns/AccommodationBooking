using AccommodationBooking.Domain.Users.Validators;
using AccommodationBooking.Domain.Users.Models;
using AccommodationBooking.Domain.Users.Repositories;
using FluentValidation;

namespace AccommodationBooking.Domain.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<LoginRequest> _loginValidator;
        public UserService(
            IUserRepository userRepository,
            IValidator<User> userValidator,
            IValidator<LoginRequest> loginValidator
            )
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _loginValidator = loginValidator;
        }

        public Task<List<User>> GetUsers(int page, int pageSize)
        {
            return _userRepository.GetAllUsers(page, pageSize);
        }

        public Task<User> Login(LoginRequest login)
        {
            _loginValidator.ValidateAndThrow(login);
            return _userRepository.Login(login);
        }

        // public Task Logout(string token)
        // {
        //     throw new NotImplementedException();
        // }

        public Task<User> Register(User model)
        {
            _userValidator.ValidateAndThrow(model);
            return _userRepository.Register(model);
        }
    }
}
