using AccommodationBooking.Domain.Hotels.Models;
using AccommodationBooking.Domain.Hotels.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Hotels.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Hotels.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AccommodationBookingContext _context;
    private readonly InfrastructureDomainHotelMapper _mapper;

    public HotelRepository(AccommodationBookingContext context, InfrastructureDomainHotelMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    async Task<Domain.Hotels.Models.Hotel> IHotelRepository.CreateHotel(Domain.Hotels.Models.Hotel hotel)
    {
        var infraHotel = _mapper.ToInfrastructure(hotel);
        var hotelCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == infraHotel.Id);
        infraHotel.City = hotelCity;
        infraHotel.CreatedAt = DateTime.UtcNow;
        infraHotel.UpdatedAt = DateTime.UtcNow;

        _context.Hotels.Add(infraHotel);
        await _context.SaveChangesAsync();

        var createdHotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Name.Equals(hotel.Name));
        return _mapper.ToDomain(createdHotel);
    }

    async Task IHotelRepository.DeleteHotelById(int hotellId)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == hotellId);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
    }

    Task IHotelRepository.DeleteHotelByName(string name)
    {
        throw new NotImplementedException();
    }

    Task<List<Domain.Hotels.Models.Hotel>> IHotelRepository.GetHotels(int page, int pageSize, HotelFilters hotelFilters)
    {
        return _context.Hotels
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
            .Include(h => h.City)
            .Select(hotel => _mapper.ToDomain(hotel))
            .ToListAsync();
    }

    async Task<Domain.Hotels.Models.Hotel> IHotelRepository.GetHotelById(int id)
    {
        var returnedHotel = await _context.Hotels.FirstOrDefaultAsync(hotel => hotel.Id == id);
        return _mapper.ToDomain(returnedHotel);
    }

    async Task<Domain.Hotels.Models.Hotel> IHotelRepository.GetHotelByName(string name)
    {
        var returnedHotel = await _context.Hotels.FirstOrDefaultAsync(Hotel => Hotel.Name.Equals(name));
        return _mapper.ToDomain(returnedHotel);
    }

    async Task<Domain.Hotels.Models.Hotel> IHotelRepository.UpdateHotel(int hotelId, Domain.Hotels.Models.Hotel hotel)
    {
        var infraHotel = _mapper.ToInfrastructure(hotel);
        var hotelToUpdate = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == hotelId);

        hotelToUpdate.Name = hotel.Name;
        hotelToUpdate.Address = hotel.Address;
        hotelToUpdate.Description = hotel.Description;
        hotelToUpdate.CityId = hotel.CityId ?? 0;
        hotelToUpdate.City = await _context.Cities.FirstOrDefaultAsync(c => c.Id == hotel.CityId);
        hotelToUpdate.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        var updatedHotel = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == hotelId);
        return _mapper.ToDomain(updatedHotel);
    }
}
