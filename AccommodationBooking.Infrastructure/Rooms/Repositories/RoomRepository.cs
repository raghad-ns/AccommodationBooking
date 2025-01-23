﻿using AccommodationBooking.Library.Pagination.Models;
using DomainRoom = AccommodationBooking.Domain.Rooms.Models.Room;
using DomainRoomFilters = AccommodationBooking.Domain.Rooms.Models.RoomFilters;
using AccommodationBooking.Domain.Rooms.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Rooms.Mappers;
using Microsoft.EntityFrameworkCore;
using AccommodationBooking.Infrastructure.Rooms.Models;
using AccommodationBooking.Domain.Rooms.Exceptions.BadRequest;

namespace AccommodationBooking.Infrastructure.Rooms.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AccommodationBookingContext _context;

    public RoomRepository(AccommodationBookingContext context)
    {
        _context = context;
    }

    async Task<int> IRoomRepository.InsertOne(DomainRoom room)
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

        throw new RoomDoesntExist();
    }

    async Task<DomainRoom> IRoomRepository.GetOne(int id, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        if (room is null) throw new RoomDoesntExist();

        return room.ToDomain();
    }

    async Task<DomainRoom> IRoomRepository.GetOneByNumber(string number, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNo == number, cancellationToken);

        if (room is null) throw new RoomDoesntExist();

        return room.ToDomain();
    }

    async Task<PaginatedData<DomainRoom>> IRoomRepository.Search(int page, int pageSize, DomainRoomFilters roomFilters, CancellationToken cancellationToken)
    {
        IQueryable<Room> baseQuery = _context.Rooms;
        baseQuery = ApplySearchFilters(baseQuery, roomFilters);

        var rooms = await baseQuery
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(r => r.ToDomain())
            .ToListAsync(cancellationToken);

        return new PaginatedData<DomainRoom>
        {
            Total = _context.Rooms.Count(),
            Data = rooms.AsReadOnly()
        };
    }

    private IQueryable<Room> ApplySearchFilters(IQueryable<Room> baseQuery, DomainRoomFilters roomFilters)
    {
        if (roomFilters.Id is not null)
            baseQuery = baseQuery.Where(room => room.Id == roomFilters.Id);

        if (roomFilters.RoomNo is not null)
            baseQuery = baseQuery.Where(room => room.RoomNo.ToLower().Equals(roomFilters.RoomNo.ToLower()));

        if (roomFilters.Description is not null)
            baseQuery = baseQuery.Where(room => room.Description.ToLower().Equals(roomFilters.Description.ToLower()));

        if (roomFilters.HotelName is not null)
            baseQuery = baseQuery.Where(room => room.Hotel.Name.ToLower().Equals(roomFilters.HotelName.ToLower()));

        if (roomFilters.AdultsCapacityFrom is not null)
            baseQuery = baseQuery.Where(room => room.AdultsCapacity >= roomFilters.AdultsCapacityFrom);

        if (roomFilters.AdultsCapacityTo is not null)
            baseQuery = baseQuery.Where(room => room.AdultsCapacity <= roomFilters.AdultsCapacityTo);

        if (roomFilters.ChildrenCapacityTo is not null)
            baseQuery = baseQuery.Where(room => room.ChildrenCapacity <= roomFilters.ChildrenCapacityTo);

        if (roomFilters.ChildrenCapacityFrom is not null)
            baseQuery = baseQuery.Where(room => room.ChildrenCapacity >= roomFilters.ChildrenCapacityFrom);

        return baseQuery;
    }

    async Task<DomainRoom> IRoomRepository.UpdateOne(int roomId, DomainRoom room)
    {
        var roomToUpdate = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
        if (roomToUpdate != null)
        {
            RoomMapper.updateFromDomain(room, roomToUpdate);

            await _context.SaveChangesAsync(CancellationToken.None);

            return roomToUpdate.ToDomain();
        }
        else throw new RoomDoesntExist();
    }
}