using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Infrastructure.BaseEntity.Models;
using AccommodationBooking.Infrastructure.Cities.Models;
using AccommodationBooking.Infrastructure.Reviews.Models;
using AccommodationBooking.Infrastructure.Rooms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccommodationBooking.Infrastructure.Hotels.Models;

[Index(nameof(Name), IsUnique = true)]
public class Hotel : AuditEntity
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
    public List<string> Images { get; set; } = new List<string>();
    public List<Amenity> Amenities { get; set; } = new List<Amenity>();
    public List<Room> Rooms { get; set; } = new List<Room>(); // Navigation property
    public List<Review> Reviews { get; set; } = new List<Review>();

    public class HotelEntityTypeConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder
                .HasMany(hotel => hotel.Rooms)
                .WithOne(room => room.Hotel)
                .HasForeignKey(room => room.HotelId)
                .HasPrincipalKey(hotel => hotel.Id);

            builder
                .HasMany(hotel => hotel.Reviews)
                .WithOne(review => review.Hotel)
                .HasForeignKey(review => review.HotelId)
                .HasPrincipalKey(hotel => hotel.Id);
        }
    }
}