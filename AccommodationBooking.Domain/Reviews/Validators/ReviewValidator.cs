using AccommodationBooking.Domain.Reviews.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.Reviews.Validators;

public class ReviewValidator: AbstractValidator<Review>
{
    public ReviewValidator()
    {
        RuleFor(h => h.UserId).NotEmpty();
        RuleFor(h => h.HotelId).NotEmpty();
        RuleFor(h => h.RoomId).NotEmpty();
        RuleFor(h => h.StarRating).NotEmpty().LessThanOrEqualTo(5).GreaterThanOrEqualTo(0);
    }
}