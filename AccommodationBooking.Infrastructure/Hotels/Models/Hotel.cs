using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Infrastructure.Cities.Models;
using AccommodationBooking.Infrastructure.Rooms.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccommodationBooking.Infrastructure.Hotels.Models;

public class Hotel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; }
    public City City { get; set; } // Navigation property
    public int CityId { get; set; } // Foreign key
    public double StarRating { get; set; }
    public List<string> Images { get; set; } = new();
    public List<Amenity> Amenities { get; set; } = new();
    public List<Room> Rooms { get; set; } // Navigation property
    public DateTime CreatedAt  { get; set; }
    public DateTime UpdatedAt { get; set; }
}
