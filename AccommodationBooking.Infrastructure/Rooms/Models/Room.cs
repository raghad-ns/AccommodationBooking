using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Infrastructure.Hotels.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AccommodationBooking.Infrastructure.Reviews.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AccommodationBooking.Infrastructure.BaseEntity.Models;

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
    public List<Review> Reviews { get; set; } = new List<Review>();

    public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder
                .HasMany(room => room.Reviews)
                .WithOne(review => review.Room)
                .HasForeignKey(review => review.RoomId)
                .HasPrincipalKey(room => room.Id);
        }
    }
}