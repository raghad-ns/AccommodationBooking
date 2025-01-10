using AccommodationBooking.Domain.Hotels.Models;

namespace AccommodationBooking.Domain.Cities.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string? PostOfficeCode { get; set; }
    public List<HotelSummary> Hotels { get; set; } = new();
}
