using AccommodationBooking.Infrastructure.Cities.Models;
using AccommodationBooking.Infrastructure.Hotels.Mappers;
using DomainCity = AccommodationBooking.Domain.Cities.Models.City;
using DomainCityFilters = AccommodationBooking.Domain.Cities.Models.CityFilters;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Cities.Mappers;

[Mapper]
public partial class InfrastructureDomainCityMapper
{
    private readonly InfrastructureDomainHotelMapper _hotelMapper;

    public InfrastructureDomainCityMapper(InfrastructureDomainHotelMapper mapper)
    {
        _hotelMapper = mapper;
    }
    public DomainCity ToDomainCity(City city) =>
        new DomainCity
        {
            Id = city.Id,
            Name = city.Name,
            Country = city.Country,
            PostOfficeCode = city.PostOfficeCode,
        };

    public DomainCity ToDomainCityIncludeHotels(City city) =>
        new DomainCity
        {
            Id = city.Id,
            Name = city.Name,
            Country = city.Country,
            PostOfficeCode = city.PostOfficeCode,
            Hotels = city.Hotels.Select(h => _hotelMapper.ToDomain(h)).ToList()
        };

    public partial City ToInfrastructureCity(DomainCity city);

    public partial CityFilters ToInfrastructure(DomainCityFilters cityFilters);
}