using AccommodationBooking.Domain.User.Models;
using AccommodationBooking.Domain.User.Repositories;
using AccommodationBooking.Domain.User.Validators;
using FluentValidation;
using System.Reflection;

namespace AccommodationBooking.Domain.User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserModel> _userValidator;
        public UserService(IUserRepository userRepository, IValidator<UserModel> userValidator)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
        }

        public Task<List<UserModel>> GetUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public Task<UserModel> Login(LoginDTO loginDTO)
        {
            var validator = new LoginValidator();
            if (!validator.Validate(loginDTO).IsValid)
            {
                return null;
            }
            return _userRepository.Login(loginDTO);
        }

        // public Task Logout(string token)
        // {
        //     throw new NotImplementedException();
        // }

        public Task<UserModel> Register(UserModel model)
        {
            if (!_userValidator.Validate(model).IsValid)
            {
                return null;
            }
            return _userRepository.Register(model);
        }
    }
}
