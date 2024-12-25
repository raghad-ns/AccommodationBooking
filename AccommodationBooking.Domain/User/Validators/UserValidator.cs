using AccommodationBooking.Domain.User.Models;
using AccommodationBooking.Domain.User.Services;
using FluentValidation;
namespace AccommodationBooking.Domain.User.Validators;

public class UserValidator: AbstractValidator<UserModel>
{
    public UserValidator() {
        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(user => user.UserName)
            .NotEmpty();

        RuleFor(user => user.FirstName)
            .NotEmpty();

        RuleFor(user => user.Password)
            .NotEmpty();
        
        RuleFor(user => user.Role)
            .NotNull();
    }
}