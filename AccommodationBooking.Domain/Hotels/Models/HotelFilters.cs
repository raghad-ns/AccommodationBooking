namespace AccommodationBooking.Domain.Hotels.Models;

public class HotelFilters
{
    public int? Id { get; set; } = null;
    public string? Name { get; set; } = null;
    public string? Description { get; set; } = null;
    public string? Address { get; set; } = null;
    public string? City { get; set; } = null;
    public double? StarRatingGreaterThanOrEqual { get; set; } = null;
    public double? StartRatingLessThanOrEqual { get; set; } = null;
    public IReadOnlyCollection<string> Amenities { get; set; } = null;
}
