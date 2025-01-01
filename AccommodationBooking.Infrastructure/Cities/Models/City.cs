using AccommodationBooking.Infrastructure.Hotels.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccommodationBooking.Infrastructure.Cities.Models;

public class City
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string? PostOfficeCode { get; set; }
    public List<Hotel>? Hotels { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
