using AccommodationBooking.Domain.Rooms.Models;
using AccommodationBooking.Domain.Rooms.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Rooms.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Rooms.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AccommodationBookingContext _context;

    public RoomRepository(AccommodationBookingContext context)
    {
        _context = context;
    }

    async Task<Room> IRoomRepository.AddOne(Room room)
    {
        var infraRoom = room.ToInfrastructure();
        var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == room.HotelId);
        infraRoom.Hotel = hotel;
        infraRoom.CreatedAt = DateTime.UtcNow;
        infraRoom.UpdatedAt = DateTime.UtcNow;

        _context.Rooms.Add(infraRoom);
        await _context.SaveChangesAsync();

        var createdRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNo.Equals(room.RoomNo));
        return createdRoom.ToDomain();
    }

    async Task IRoomRepository.DeleteOne(int roomId)
    {
        var roomToBeDeleted = await _context.Rooms.FindAsync(roomId);
        if (roomToBeDeleted != null)
        {
            _context.Rooms.Remove(roomToBeDeleted);
            await _context.SaveChangesAsync();
        }
    }

    async Task<Room> IRoomRepository.GetOne(int id)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        return room.ToDomain();
    }

    async Task<Room> IRoomRepository.GetOneByNumber(string number)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNo == number);
        return room.ToDomain();
    }

    async Task<List<Room>> IRoomRepository.Search(int page, int pageSize, RoomFilters roomFilters)
    {
        return await _context.Rooms
            .Where( r => (
                (roomFilters.Id != null? r.Id == roomFilters.Id : true) &&
                (roomFilters.RoomNo != null? r.RoomNo.ToLower().Contains(roomFilters.RoomNo.ToLower()) : true) &&
                (roomFilters.Description != null? r.Description.ToLower().Contains(roomFilters.Description.ToLower()) : true) &&
                (roomFilters.AdultsCapacityFrom != null? r.AdultsCapacity >= roomFilters.AdultsCapacityFrom : true) && 
                (roomFilters.AdultsCapacityTo != null? r.AdultsCapacity <= roomFilters.AdultsCapacityTo : true) && 
                (roomFilters.ChildrenCapacityFrom != null? r.ChildrenCapacity >= roomFilters.ChildrenCapacityFrom : true) && 
                (roomFilters.ChildrenCapacityTo != null? r.ChildrenCapacity <= roomFilters.ChildrenCapacityTo : true) 
             ))
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(r => r.ToDomain())
            .ToListAsync();
    }

    async Task<Room> IRoomRepository.UpdateOne(int roomId, Room room)
    {
        var roomToUpdate = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
        if (roomToUpdate != null)
        {
            room.updateFromDomain(roomToUpdate);

            await _context.SaveChangesAsync();

            var updatedRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            return updatedRoom.ToDomain();
        }
        else throw new InvalidOperationException("Room not found");
    }
}