using AccommodationBooking.Domain.Common;
using DomainRoom = AccommodationBooking.Domain.Rooms.Models.Room;
using DomainRoomFilters = AccommodationBooking.Domain.Rooms.Models.RoomFilters;

namespace AccommodationBooking.Domain.Rooms.Repositories;

public interface IRoomRepository
{
    Task<PaginatedData<DomainRoom>> Search(int page, int pageSize, DomainRoomFilters roomFilters, CancellationToken cancellationToken);
    Task<DomainRoom> GetOne(int id, CancellationToken cancellationToken);
    Task<DomainRoom> GetOneByNumber(string number, CancellationToken cancellationToken);
    Task<int> InsertOne(DomainRoom room);
    Task<DomainRoom> UpdateOne(int roomId, DomainRoom room);
    Task DeleteOne(int roomId);
}