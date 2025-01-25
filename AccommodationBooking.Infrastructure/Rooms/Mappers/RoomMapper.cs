using AccommodationBooking.Infrastructure.Rooms.Models;
using Riok.Mapperly.Abstractions;
using DomainRoom = AccommodationBooking.Domain.Rooms.Models.Room;
using DomainRoomFilters = AccommodationBooking.Domain.Rooms.Models.RoomFilters;

namespace AccommodationBooking.Infrastructure.Rooms.Mappers;

[Mapper]
public static partial class RoomMapper
{
    public static partial Room ToInfrastructure(this DomainRoom room);
    public static partial void updateFromDomain(DomainRoom source, Room target);
    public static partial void updateFromInfrastructure(Room source, Room target);

    public static partial DomainRoom ToDomain(this Room room);

}