using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Infrastructure.Hotels.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AccommodationBooking.Infrastructure.BaseEntity.Models;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Rooms.Models;
[Index(nameof(RoomNo), nameof(HotelId))]

public class Room : AuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string RoomNo { get; set; }
    public Hotel Hotel { get; set; }
    public int HotelId { get; set; }
    public int AdultsCapacity { get; set; } = 2;
    public int ChildrenCapacity { get; set; } = 0;
    public bool IsAvailable { get; set; } = true;
    public string Description { get; set; }
    public List<string> Images { get; set; }
    public RoomType RoomType { get; set; }
}