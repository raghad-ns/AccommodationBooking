using AccommodationBooking.Domain.Hotels.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.Hotels.Validators;

public class HotelValidator : AbstractValidator<Hotel>
{
    public HotelValidator()
    {
        RuleFor(h => h.Name).NotEmpty();
        RuleFor(h => h.Address).NotEmpty();
        RuleFor(h => h.CityId).NotNull();
    }
}