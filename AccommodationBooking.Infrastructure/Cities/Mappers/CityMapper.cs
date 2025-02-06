using AccommodationBooking.Infrastructure.Cities.Models;
using Riok.Mapperly.Abstractions;
using DomainCity = AccommodationBooking.Domain.Cities.Models.City;

namespace AccommodationBooking.Infrastructure.Cities.Mappers;

[Mapper]
public static partial class CityMapper
{
    public static partial DomainCity ToDomain(this City city);

    public static partial City ToInfrastructure(this DomainCity city);
    public static partial void ToInfrastructureUpdate(DomainCity source, City target);
}