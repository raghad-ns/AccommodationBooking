using AccommodationBooking.Infrastructure.Cities.Models;
using AccommodationBooking.Infrastructure.Hotels.Mappers;
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
    public Domain.Cities.Models.City ToDomainCity(City city) =>
        new Domain.Cities.Models.City
        {
            Id = city.Id,
            Name = city.Name,
            Country = city.Country,
            PostOfficeCode = city.PostOfficeCode,
        };

    public Domain.Cities.Models.City ToDomainCityIncludeHotels(City city) =>
        new Domain.Cities.Models.City
        {
            Id = city.Id,
            Name = city.Name,
            Country = city.Country,
            PostOfficeCode = city.PostOfficeCode,
            Hotels = city.Hotels.Select(h => _hotelMapper.ToDomainHotel(h)).ToList()
        };

    public partial City ToInfrastructureCity(Domain.Cities.Models.City city);
}