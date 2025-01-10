using DomainRoom = AccommodationBooking.Domain.Rooms.Models.Room;
using DomainRoomFilters = AccommodationBooking.Domain.Rooms.Models.RoomFilters;

namespace AccommodationBooking.Domain.Rooms.Repositories;

public interface IRoomRepository
{
    Task<List<DomainRoom>> GetRooms(int page, int pageSize, DomainRoomFilters roomFilters);
    Task<DomainRoom> GetRoomById(int id);
    Task<DomainRoom> GetRoomByNumber(string number);
    Task<DomainRoom> CreateRoom(DomainRoom room);
    Task<DomainRoom> UpdateRoom(int roomId, DomainRoom room);
    Task DeleteRoomById(int roomId);
}