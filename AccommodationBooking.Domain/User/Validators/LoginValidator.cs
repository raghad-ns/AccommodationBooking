using AccommodationBooking.Domain.User.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.User.Validators;

public class LoginValidator : AbstractValidator<LoginDTO>
{
    public LoginValidator()
    {
        RuleFor(loginObject => loginObject.UserName)
            .NotEmpty();

        RuleFor(loginObject => loginObject.Password)
            .NotEmpty();
    }
}