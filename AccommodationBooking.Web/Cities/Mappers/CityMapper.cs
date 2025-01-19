using AccommodationBooking.Web.Cities.Models;
using Riok.Mapperly.Abstractions;
using DomainCity = AccommodationBooking.Domain.Cities.Models.City;
using DomainCityFilters = AccommodationBooking.Domain.Cities.Models.CityFilters;

namespace AccommodationBooking.Web.Cities.Mappers;

[Mapper]
public static partial class CityMapper
{
    public static partial DomainCity ToDomain(this City hotel);
    public static partial City ToApplication(this DomainCity city);
    public static partial DomainCityFilters ToDomain(this CityFilters hotelFilters);
}