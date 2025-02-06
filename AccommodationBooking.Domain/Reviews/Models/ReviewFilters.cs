namespace AccommodationBooking.Domain.Reviews.Models;

public class ReviewFilters
{
    public int? Id { get; set; }
    public Guid? UserId { get; set; }
    public int? HotelId { get; set; }
    public int? RoomId { get; set; }
    public double? StartRatingFrom { get; set; }
    public double? StarRatingTo { get; set; }
    public string? Comment { get; set; }
}