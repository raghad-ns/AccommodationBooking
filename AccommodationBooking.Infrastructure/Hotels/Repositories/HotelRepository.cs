using DomainHotel = AccommodationBooking.Domain.Hotels.Models.Hotel;
using DomainHotelFilters = AccommodationBooking.Domain.Hotels.Models.HotelFilters;
using AccommodationBooking.Domain.Hotels.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Hotels.Mappers;
using Microsoft.EntityFrameworkCore;
using AccommodationBooking.Domain.Common;

namespace AccommodationBooking.Infrastructure.Hotels.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AccommodationBookingContext _context;

    public HotelRepository(AccommodationBookingContext context)
    {
        _context = context;
    }

    async Task<DomainHotel> IHotelRepository.AddOne(DomainHotel hotel)
    {
        var infraHotel = hotel.ToInfrastructure();
        var hotelCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == infraHotel.Id);
        infraHotel.City = hotelCity;
        infraHotel.CreatedAt = DateTime.UtcNow;
        infraHotel.UpdatedAt = DateTime.UtcNow;

        _context.Hotels.Add(infraHotel);
        await _context.SaveChangesAsync();

        var createdHotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Name.Equals(hotel.Name));
        return createdHotel.ToDomain();
    }

    async Task IHotelRepository.DeleteOne(int hotellId)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == hotellId);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
    }

    async Task<PaginatedData<DomainHotel>> IHotelRepository.Search(int page, int pageSize, DomainHotelFilters hotelFilters)
    {
        var hotels = await _context.Hotels
            .Where(h => (
                (hotelFilters.Id != null ? h.Id == hotelFilters.Id : true) &&

                (hotelFilters.Name != null ? h.Name.ToLower().Contains(hotelFilters.Name.ToLower()) : true) &&

                (hotelFilters.Description != null ? h.Description.ToLower().Contains(hotelFilters.Description.ToLower()) : true) &&

                (hotelFilters.City != null ? h.City.Name.ToLower().Contains(hotelFilters.City.ToLower()) : true) &&

                (hotelFilters.StarRatingGreaterThanOrEqual != null ? h.StarRating >= hotelFilters.StartRatingLessThanOrEqual : true) &&

                (hotelFilters.StartRatingLessThanOrEqual != null ? h.StarRating <= hotelFilters.StartRatingLessThanOrEqual : true)
            ))
            .Skip(page * pageSize)
            .Take(pageSize)
            .Include(h => h.Rooms)
            .Select(hotel => hotel.ToDomain())
            .ToListAsync();

        return new PaginatedData<DomainHotel>
        {
            Total = _context.Hotels.Count(),
            Data = hotels.AsReadOnly()
        };
    }

    async Task<DomainHotel> IHotelRepository.GetOne(int id)
    {
        var returnedHotel = await _context.Hotels.FirstOrDefaultAsync(hotel => hotel.Id == id);
        return returnedHotel.ToDomain();
    }

    async Task<DomainHotel> IHotelRepository.GetOneByName(string name)
    {
        var returnedHotel = await _context.Hotels.FirstOrDefaultAsync(Hotel => Hotel.Name.Equals(name));
        return returnedHotel.ToDomain();
    }

    async Task<DomainHotel> IHotelRepository.UpdateOne(int hotelId, DomainHotel hotel)
    {
        var hotelToUpdate = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == hotelId);

        hotel.ToInfrastructureUpdate(hotelToUpdate);

        await _context.SaveChangesAsync();

        var updatedHotel = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == hotelId);
        return updatedHotel.ToDomain();
    }
}