using DomainHotel = AccommodationBooking.Domain.Hotels.Models.Hotel;
using DomainHotelFilters = AccommodationBooking.Domain.Hotels.Models.HotelFilters;
using AccommodationBooking.Infrastructure.Cities.Mappers;
using AccommodationBooking.Infrastructure.Hotels.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Hotels.Mappers;

[Mapper]
public partial class InfrastructureDomainHotelMapper
{

    public DomainHotel ToDomain(Hotel hotel) =>
        new DomainHotel
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Address = hotel.Address,
            Description = hotel.Description,
            CityId = hotel.CityId,
            Amenities = hotel.Amenities,
            Images = hotel.Images,
            StarRating = hotel.StarRating,
        };

    public Hotel ToInfrastructure(DomainHotel hotel) => 
        new Hotel
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Address = hotel.Address,
            Description = hotel.Description,
            CityId = hotel.CityId ?? 0,
            Amenities = hotel.Amenities,
            Images = hotel.Images,
            StarRating = hotel.StarRating,
        };

    public partial HotelFilters ToInfrastructure(DomainHotelFilters hotelFilters);
}