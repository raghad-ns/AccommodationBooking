using DomainHotel = AccommodationBooking.Domain.Hotels.Models.Hotel;
using AccommodationBooking.Infrastructure.Hotels.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Infrastructure.Hotels.Mappers;

[Mapper]
public static partial class InfrastructureDomainHotelMapper
{
    public static partial DomainHotel ToDomain(this Hotel hotel);

    public static partial Hotel ToInfrastructure(this DomainHotel hotel);
    public static partial void ToInfrastructureUpdate(this DomainHotel source, Hotel target);
}