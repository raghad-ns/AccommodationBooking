using AccommodationBooking.Infrastructure.Cities.Mappers;
using AccommodationBooking.Infrastructure.Hotels.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Hotels.Mappers;

[Mapper]
public partial class InfrastructureDomainHotelMapper
{

    public Domain.Hotels.Models.Hotel ToDomainHotel(Hotel hotel) =>
        new Domain.Hotels.Models.Hotel
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

    public Hotel ToInfrastructureHotel(Domain.Hotels.Models.Hotel hotel) => 
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
}