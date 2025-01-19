using AccommodationBooking.Application.Rooms.Models;
using AccommodationBooking.Web.Rooms.Models;
using Riok.Mapperly.Abstractions;
using DomainRoom = AccommodationBooking.Domain.Rooms.Models.Room;
using DomainRoomFilters = AccommodationBooking.Domain.Rooms.Models.RoomFilters;

namespace AccommodationBooking.Web.Rooms.Mappers;

[Mapper]
public static partial class RoomMapper
{
    public static partial Room ToApplication(this DomainRoom room);
    public static partial DomainRoom ToDomain(this Room room);

    public static partial RoomFilters ToApplication(this DomainRoomFilters roomFilters);
    public static partial DomainRoomFilters ToDomain(this RoomFilters roomFilters);
}