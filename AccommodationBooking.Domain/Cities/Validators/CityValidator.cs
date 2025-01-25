using AccommodationBooking.Domain.Cities.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.Cities.Validators;
public class CityValidator : AbstractValidator<City>
{
    public CityValidator()
    {
        RuleFor(city => city.Name).NotEmpty();
        RuleFor(city => city.Country).NotEmpty();
    }
}