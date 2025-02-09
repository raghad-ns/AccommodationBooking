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
    public IReadOnlyCollection<string> Images { get; } 
    public IReadOnlyCollection<Amenity> Amenities { get; } 
    public IReadOnlyCollection<Room> Rooms { get; } // Navigation property
    public IReadOnlyCollection<Review> Reviews { get; } 
}

internal class HotelEntityTypeConfiguration : IEntityTypeConfiguration<Hotel>
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