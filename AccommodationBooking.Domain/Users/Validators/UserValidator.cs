using AccommodationBooking.Domain.Users.Services;
using FluentValidation;
namespace AccommodationBooking.Domain.Users.Validators;

public class UserValidator : AbstractValidator<Models.User>
{
    public UserValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(user => user.UserName)
            .NotEmpty();

        RuleFor(user => user.FirstName)
            .NotEmpty();

        RuleFor(user => user.Password)
            .NotEmpty();
    }
}