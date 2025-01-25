using AccommodationBooking.Domain.Rooms.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.Rooms.Validators;

public class RoomValidator : AbstractValidator<Room>
{
    public RoomValidator()
    {
        RuleFor(room => room.RoomNo).NotEmpty();
        RuleFor(room => room.HotelId).NotNull();
        RuleFor(room => room.RoomType).NotNull();
    }
}