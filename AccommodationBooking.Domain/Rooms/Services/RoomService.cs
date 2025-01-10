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
    async Task<Room> IRoomService.CreateRoom(Room room)
    {
        return await _roomRepository.CreateRoom(room);
    }

    async Task IRoomService.DeleteRoomById(int roomId)
    {
        await _roomRepository.DeleteRoomById(roomId);
    }

    async Task<Room> IRoomService.GetRoomById(int id)
    {
        return await _roomRepository.GetRoomById(id);
    }

    async Task<Room> IRoomService.GetRoomByNumber(string number)
    {
        return await _roomRepository.GetRoomByNumber(number);
    }

    async Task<List<Room>> IRoomService.GetRooms(int page, int pageSize, RoomFilters roomFilters)
    {
        return await _roomRepository.GetRooms(page, pageSize, roomFilters);
    }

    async Task<Room> IRoomService.UpdateRoom(int roomId, Room room)
    {
        return await _roomRepository.UpdateRoom(roomId, room);
    }
}