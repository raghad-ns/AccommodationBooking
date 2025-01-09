using AccommodationBooking.Application.Cities.Models;
using Riok.Mapperly.Abstractions;
using DomainCity = AccommodationBooking.Domain.Cities.Models.City;
using DomainCityFilters = AccommodationBooking.Domain.Cities.Models.CityFilters;

namespace AccommodationBooking.Application.Cities.Mappers;

[Mapper]
public partial class CityMapper
{
    public partial DomainCity ToDomain(City hotel);
    public partial City ToApplication(DomainCity city);
    public partial DomainCityFilters ToDomain(CityFilters hotelFilters);
}