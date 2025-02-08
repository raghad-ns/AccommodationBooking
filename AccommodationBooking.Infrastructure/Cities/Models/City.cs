using AccommodationBooking.Infrastructure.BaseEntity.Models;
using AccommodationBooking.Infrastructure.Hotels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccommodationBooking.Infrastructure.Cities.Models;

[Index(nameof(Name), nameof(Country), nameof(PostOfficeCode))]
public class City : AuditEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string? PostOfficeCode { get; set; }
    public IReadOnlyCollection<Hotel> Hotels { get; } = Array.Empty<Hotel>();
}

internal class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
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