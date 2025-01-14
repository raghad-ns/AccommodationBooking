using AccommodationBooking.Domain.Users.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.Users.Validators;

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