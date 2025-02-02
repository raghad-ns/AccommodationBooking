using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Library.Pagination.Models;

namespace AccommodationBooking.Domain.Rooms.Services;

public interface IRoomService
{
    Task<PaginatedData<Room>> Search(int page, int pageSize, RoomFilters roomFilters, CancellationToken cancellationToken);
    Task<Room> GetOne(int id, CancellationToken cancellationToken);
    Task<Room> GetOneByNumber(string number, CancellationToken cancellationToken);
    Task<Room> InsertOne(Room room, CancellationToken cancellationToken);
    Task<Room> UpdateOne(int roomId, Room room);
    Task DeleteOne(int roomId);
}