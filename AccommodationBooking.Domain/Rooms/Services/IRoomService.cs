using AccommodationBooking.Domain.Rooms.Models;

namespace AccommodationBooking.Domain.Rooms.Services;

public interface IRoomService
{
    Task<List<Room>> GetRooms(int page, int pageSize, RoomFilters roomFilters);
    Task<Room> GetRoomById(int id);
    Task<Room> GetRoomByNumber(string number);
    Task<Room> CreateRoom(Room room);
    Task<Room> UpdateRoom(int roomId, Room room);
    Task DeleteRoomById(int roomId);
}