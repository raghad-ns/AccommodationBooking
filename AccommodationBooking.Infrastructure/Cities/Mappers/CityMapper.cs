using AccommodationBooking.Infrastructure.Cities.Models;
using AccommodationBooking.Infrastructure.Hotels.Mappers;
using DomainCity = AccommodationBooking.Domain.Cities.Models.City;
using DomainCityFilters = AccommodationBooking.Domain.Cities.Models.CityFilters;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Cities.Mappers;

[Mapper]
public static partial class CityMapper
{
    public static partial DomainCity ToDomain(this City city);

    public static partial City ToInfrastructure(this DomainCity city);
    public static partial void ToInfrastructureUpdate(this DomainCity source, City target);
}