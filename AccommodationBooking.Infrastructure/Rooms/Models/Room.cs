using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Infrastructure.BaseEntity.Models;
using AccommodationBooking.Infrastructure.Hotels.Models;
using AccommodationBooking.Infrastructure.Reviews.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public IReadOnlyCollection<string> Images { get; }
    public RoomType RoomType { get; set; }
    public IReadOnlyCollection<Review> Reviews { get; } = new List<Review>();
}

internal class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
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