using AccommodationBooking.Domain.Hotels.Models;

namespace AccommodationBooking.Application.Hotels.Models;

public class HotelFilters
{
    public int? Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Address { get; init; }
    public string? City { get; init; }
    public double? StarRatingGreaterThanOrEqual { get; init; }
    public double? StartRatingLessThanOrEqual { get; init; }
    public List<string> Amenities { get; init; }
}