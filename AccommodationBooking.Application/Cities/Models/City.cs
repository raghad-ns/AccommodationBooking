using AccommodationBooking.Application.Hotels.Models;

namespace AccommodationBooking.Application.Cities.Models;

public class City
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Country { get; init; }
    public string? PostOfficeCode { get; init; }
}
