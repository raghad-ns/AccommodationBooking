using AccommodationBooking.Infrastructure.Hotels.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AccommodationBooking.Infrastructure.BaseEntity.Models;

namespace AccommodationBooking.Infrastructure.Cities.Models;

[Index(nameof(Name), nameof(Country), nameof(PostOfficeCode))]
public class City: AuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string? PostOfficeCode { get; set; }
    public List<Hotel> Hotels { get; set; } = new List<Hotel>();

    public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder
                .HasMany(city => city.Hotels)
                .WithOne(hotel => hotel.City)
                .HasForeignKey(hotel => hotel.CityId)
                .HasPrincipalKey(city => city.Id);
        }
    }
}