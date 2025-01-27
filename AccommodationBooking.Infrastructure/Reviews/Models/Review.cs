using AccommodationBooking.Infrastructure.Hotels.Models;
using AccommodationBooking.Infrastructure.Rooms.Models;
using AccommodationBooking.Infrastructure.Users.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AccommodationBooking.Infrastructure.BaseEntity.Models;

namespace AccommodationBooking.Infrastructure.Reviews.Models;

public class Review: AuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Hotel Hotel { get; set; }
    public int HotelId { get; set; }
    public Room Room { get; set; }
    public int RoomId { get; set; }
    public string? Comment { get; set; } = string.Empty;
    public double? StarRating { get; set; }
}