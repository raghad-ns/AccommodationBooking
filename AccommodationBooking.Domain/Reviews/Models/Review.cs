namespace AccommodationBooking.Domain.Reviews.Models;

public class Review
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int HotelId { get; set; }
    public int RoomId { get; set; }
    public string? Comment { get; set; } = string.Empty;
    public double? StarRating { get; set; }
}