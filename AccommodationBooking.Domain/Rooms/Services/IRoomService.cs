using AccommodationBooking.Domain.Common;
using AccommodationBooking.Domain.Rooms.Models;

namespace AccommodationBooking.Domain.Rooms.Services;

public interface IRoomService
{
    Task<PaginatedData<Room>> Search(int page, int pageSize, RoomFilters roomFilters);
    Task<Room> GetOne(int id);
    Task<Room> GetOneByNumber(string number);
    Task<Room> AddOne(Room room);
    Task<Room> UpdateOne(int roomId, Room room);
    Task DeleteOne(int roomId);
}