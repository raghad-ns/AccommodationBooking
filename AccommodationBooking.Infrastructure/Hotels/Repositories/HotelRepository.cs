using DomainHotel = AccommodationBooking.Domain.Hotels.Models.Hotel;
using DomainHotelFilters = AccommodationBooking.Domain.Hotels.Models.HotelFilters;
using AccommodationBooking.Domain.Hotels.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Hotels.Mappers;
using Microsoft.EntityFrameworkCore;
using AccommodationBooking.Library.Pagination.Models;
using AccommodationBooking.Infrastructure.Hotels.Models;

namespace AccommodationBooking.Infrastructure.Hotels.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AccommodationBookingContext _context;

    public HotelRepository(AccommodationBookingContext context)
    {
        _context = context;
    }

    async Task<int> IHotelRepository.InsertOne(DomainHotel hotel)
    {
        var infraHotel = hotel.ToInfrastructure();
        var hotelCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == infraHotel.Id);
        infraHotel.City = hotelCity;

        _context.Hotels.Add(infraHotel);
        await _context.SaveChangesAsync(CancellationToken.None);

        return infraHotel.Id;
    }

    async Task IHotelRepository.DeleteOne(int hotellId)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == hotellId);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }

    async Task<PaginatedData<DomainHotel>> IHotelRepository.Search(
        int page, 
        int pageSize, 
        DomainHotelFilters hotelFilters, 
        CancellationToken cancellationToken
        )
    {
        IQueryable<Hotel>  baseQuery = _context.Hotels;
        baseQuery = ApplySearchFilters(baseQuery, hotelFilters);

        var hotels = await baseQuery
            .Skip(page * pageSize)
            .Take(pageSize)
            .Include(h => h.Rooms)
            .Select(hotel => hotel.ToDomain())
            .ToListAsync(cancellationToken);

        return new PaginatedData<DomainHotel>
        {
            Total = _context.Hotels.Count(),
            Data = hotels.AsReadOnly()
        };
    }

    private IQueryable<Hotel> ApplySearchFilters(IQueryable<Hotel> baseQuery, DomainHotelFilters hotelFilters)
    {
        if (hotelFilters.Id is not null)
            baseQuery = baseQuery.Where(hotel => hotel.Id == hotelFilters.Id);

        if (hotelFilters.Name is not null)
            baseQuery = baseQuery.Where(hotel => hotel.Name.ToLower().Equals(hotelFilters.Name.ToLower()));

        if (hotelFilters.Address is not null)
            baseQuery = baseQuery.Where(hotel => hotel.Address.ToLower().Equals(hotelFilters.Address.ToLower()));

        if (hotelFilters.StarRatingGreaterThanOrEqual is not null)
            baseQuery = baseQuery.Where(hotel => hotel.StarRating <= hotelFilters.StartRatingLessThanOrEqual);

        if (hotelFilters.StartRatingLessThanOrEqual is not null)
            baseQuery = baseQuery.Where(hotel => hotel.StarRating >= hotelFilters.StartRatingLessThanOrEqual);

        return baseQuery;
    }

    async Task<DomainHotel> IHotelRepository.GetOne(int id, CancellationToken cancellationToken)
    {
        var returnedHotel = await _context.Hotels.FirstOrDefaultAsync(hotel => hotel.Id == id, cancellationToken);
        return returnedHotel.ToDomain();
    }

    async Task<DomainHotel> IHotelRepository.GetOneByName(string name, CancellationToken cancellationToken)
    {
        var returnedHotel = await _context.Hotels.FirstOrDefaultAsync(Hotel => Hotel.Name.Equals(name), cancellationToken);
        return returnedHotel.ToDomain();
    }

    async Task<DomainHotel> IHotelRepository.UpdateOne(int hotelId, DomainHotel hotel)
    {
        var hotelToUpdate = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == hotelId);

        HotelMapper.ToInfrastructureUpdate(hotel, hotelToUpdate);

        await _context.SaveChangesAsync(CancellationToken.None);

        return hotelToUpdate.ToDomain();
    }
}