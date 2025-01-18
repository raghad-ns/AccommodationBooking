﻿using AccommodationBooking.Library.Pagination.Models;
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

    async Task<int> IRoomRepository.InsertOne(Room room)
    {
        var infraRoom = room.ToInfrastructure();
        var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == room.HotelId);
        infraRoom.Hotel = hotel;

        _context.Rooms.Add(infraRoom);
        await _context.SaveChangesAsync(CancellationToken.None);

        return infraRoom.Id;
    }

    async Task IRoomRepository.DeleteOne(int roomId)
    {
        var roomToBeDeleted = await _context.Rooms.FindAsync(roomId);
        if (roomToBeDeleted != null)
        {
            _context.Rooms.Remove(roomToBeDeleted);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }

    async Task<Room> IRoomRepository.GetOne(int id, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        return room.ToDomain();
    }

    async Task<Room> IRoomRepository.GetOneByNumber(string number, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNo == number, cancellationToken);
        return room.ToDomain();
    }

    async Task<PaginatedData<Room>> IRoomRepository.Search(int page, int pageSize, RoomFilters roomFilters, CancellationToken cancellationToken)
    {
        var rooms = await _context.Rooms
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
            .ToListAsync(cancellationToken);

        return new PaginatedData<Room>
        {
            Total = _context.Rooms.Count(),
            Data = rooms.AsReadOnly()
        };
    }

    async Task<Room> IRoomRepository.UpdateOne(int roomId, Room room)
    {
        var roomToUpdate = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
        if (roomToUpdate != null)
        {
            RoomMapper.updateFromDomain(room, roomToUpdate);

            await _context.SaveChangesAsync(CancellationToken.None);

            return roomToUpdate.ToDomain();
        }
        else throw new InvalidOperationException("Room not found");
    }
}