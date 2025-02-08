using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Domain.Rooms.Repositories;
using AccommodationBooking.Library.Pagination.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.Rooms.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<Room> _validator;

    public RoomService(IRoomRepository roomRepository, IValidator<Room> validator)
    {
        _roomRepository = roomRepository;
        _validator = validator;
    }
    public async Task<Room> InsertOne(Room room, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(room);
        var id = await _roomRepository.InsertOne(room);
        return await GetOne(id, cancellationToken);
    }

    public Task<int> DeleteOne(int roomId)
    {
        return _roomRepository.DeleteOne(roomId);
    }

    public Task<Room> GetOne(int id, CancellationToken cancellationToken)
    {
        return _roomRepository.GetOne(id, cancellationToken);
    }

    public Task<Room> GetOneByNumber(string number, CancellationToken cancellationToken)
    {
        return _roomRepository.GetOneByNumber(number, cancellationToken);
    }

    public Task<PaginatedData<Room>> Search(int page, int pageSize, RoomFilters roomFilters, CancellationToken cancellationToken)
    {
        return _roomRepository.Search(page, pageSize, roomFilters, cancellationToken);
    }

    public Task<Room> UpdateOne(int roomId, Room room)
    {
        _validator.ValidateAndThrow(room);
        return _roomRepository.UpdateOne(roomId, room);
    }
}