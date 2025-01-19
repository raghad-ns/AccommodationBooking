using AccommodationBooking.Web.Cities.Models;
using AccommodationBooking.Web.Rooms.Models;
using AccommodationBooking.Domain.Hotels.Models;

namespace AccommodationBooking.Web.Hotels.Models;

public class Hotel
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public string Address { get; init; }
    public int? CityId { get; init; }
    public double StarRating { get; init; }
    public IReadOnlyCollection<string> Images { get; init; } = new List<string>();
    public IReadOnlyCollection<Amenity> Amenities { get; init; } = new List<Amenity>();
}
