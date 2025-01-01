using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Rooms.Models;

namespace AccommodationBooking.Domain.Hotels.Models;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public City City { get; set; } 
    public double StarRating { get; set; }
    public List<string> Images { get; set; }
    public List<Amenity> Amenities { get; set; }
    public List<Room> Rooms { get; set; } 
}
