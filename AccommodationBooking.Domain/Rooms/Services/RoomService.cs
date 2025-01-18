using AccommodationBooking.Domain.Common;
using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Domain.Rooms.Repositories;

namespace AccommodationBooking.Domain.Rooms.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    async Task<Room> IRoomService.InsertOne(Room room, CancellationToken cancellationToken)
    {
        var id = await _roomRepository.InsertOne(room);
        return await _roomRepository.GetOne(id, cancellationToken);
    }

    Task IRoomService.DeleteOne(int roomId)
    {
        return _roomRepository.DeleteOne(roomId);
    }

    Task<Room> IRoomService.GetOne(int id, CancellationToken cancellationToken)
    {
        return _roomRepository.GetOne(id, cancellationToken);
    }

    Task<Room> IRoomService.GetOneByNumber(string number, CancellationToken cancellationToken)
    {
        return _roomRepository.GetOneByNumber(number, cancellationToken);
    }

    Task<PaginatedData<Room>> IRoomService.Search(int page, int pageSize, RoomFilters roomFilters, CancellationToken cancellationToken)
    {
        return _roomRepository.Search(page, pageSize, roomFilters, cancellationToken);
    }

    Task<Room> IRoomService.UpdateOne(int roomId, Room room)
    {
        return _roomRepository.UpdateOne(roomId, room);
    }
}