using DomainRoom = AccommodationBooking.Domain.Rooms.Models.Room;
using DomainRoomFilters = AccommodationBooking.Domain.Rooms.Models.RoomFilters;

namespace AccommodationBooking.Domain.Rooms.Repositories;

public interface IRoomRepository
{
    Task<List<DomainRoom>> Search(int page, int pageSize, DomainRoomFilters roomFilters);
    Task<DomainRoom> GetOne(int id);
    Task<DomainRoom> GetOneByNumber(string number);
    Task<DomainRoom> AddOne(DomainRoom room);
    Task<DomainRoom> UpdateOne(int roomId, DomainRoom room);
    Task DeleteOne(int roomId);
}