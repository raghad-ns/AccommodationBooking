using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Domain.Users.Models;

namespace AccommodationBooking.Domain.Reviews.Models;

public class Review
{
    public int Id { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Hotel Hotel { get; set; }
    public int HotelId { get; set; }
    public Room Room { get; set; }
    public int RoomId { get; set; }
    public string? Comment { get; set; } = string.Empty;
    public double? StarRating { get; set; }
}