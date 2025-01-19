using AccommodationBooking.Web.Hotels.Models;
using Riok.Mapperly.Abstractions;
using DomainHotel = AccommodationBooking.Domain.Hotels.Models.Hotel;
using DomainHotelFilters = AccommodationBooking.Domain.Hotels.Models.HotelFilters;

namespace AccommodationBooking.Web.Hotels.Mappers;

[Mapper]
public static partial class HotelMapper
{
    public static partial DomainHotel ToDomain(this Hotel hotel);
    public static partial Hotel ToApplication(this DomainHotel hotel);
    public static partial DomainHotelFilters ToDomain(this HotelFilters hotelFilters);
}