using AccommodationBooking.Domain.Users.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.User.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(loginObject => loginObject.UserName)
            .NotEmpty();

        RuleFor(loginObject => loginObject.Password)
            .NotEmpty();
    }
}