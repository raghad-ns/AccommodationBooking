using DomainHotel = AccommodationBooking.Domain.Hotels.Models.Hotel;
using DomainHotelFilters = AccommodationBooking.Domain.Hotels.Models.HotelFilters;
using AccommodationBooking.Infrastructure.Hotels.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Hotels.Mappers;

[Mapper]
public static partial class InfrastructureDomainHotelMapper
{

    public static partial DomainHotel ToDomain(this Hotel hotel);
        

    public static partial Hotel ToInfrastructure(this DomainHotel hotel);
}