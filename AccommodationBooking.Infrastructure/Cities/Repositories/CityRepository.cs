using AccommodationBooking.Infrastructure.Cities.Mappers;
using DomainCity = AccommodationBooking.Domain.Cities.Models.City;
using DomainCityFilters = AccommodationBooking.Domain.Cities.Models.CityFilters;
using AccommodationBooking.Domain.Cities.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using AccommodationBooking.Library.Pagination.Models;

namespace AccommodationBooking.Infrastructure.Cities.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AccommodationBookingContext _context;

    public CityRepository(AccommodationBookingContext context)
    {
        _context = context;
    }

    async Task<int> ICityRepository.InsertOne(DomainCity city)
    {
        var infraCity = city.ToInfrastructure();

        _context.Cities.Add(infraCity);
        await _context.SaveChangesAsync(CancellationToken.None);

        return infraCity.Id;
    }

    async Task ICityRepository.DeleteOne(int cityId)
    {
        var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
        if (city != null)
        {
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }

    async Task<PaginatedData<DomainCity>> ICityRepository.Search(
        int page,
        int pageSize,
        DomainCityFilters cityFilters,
        CancellationToken cancellationToken
        )
    {
        var cities = await _context.Cities
            .Where(c => (
                (cityFilters.Id != null ? c.Id == cityFilters.Id : true) &&
                (cityFilters.Name != null ? c.Name.ToLower().Contains(cityFilters.Name.ToLower()) : true) &&
                (cityFilters.Country != null ? c.Country.ToLower().Contains(cityFilters.Country.ToLower()) : true) &&
                (cityFilters.PostOfficeCode != null ? c.PostOfficeCode.ToLower().Contains(cityFilters.PostOfficeCode.ToLower()) : true)
            ))
            .Skip(page * pageSize)
            .Take(pageSize)
            .Include(c => c.Hotels)
            .Select(city => city.ToDomain())
            .ToListAsync(cancellationToken);

        return new PaginatedData<DomainCity>
        {
            Total = _context.Cities.Count(),
            Data = cities.AsReadOnly()
        };
    }

    async Task<DomainCity> ICityRepository.GetOne(int id, CancellationToken cancellationToken)
    {
        var returnedCity = await _context.Cities.FirstOrDefaultAsync(city => city.Id == id, cancellationToken);
        return returnedCity.ToDomain();
    }

    async Task<DomainCity> ICityRepository.GetOneByName(string name, CancellationToken cancellationToken)
    {
        var returnedCity = await _context.Cities.FirstOrDefaultAsync(city => city.Name.Equals(name), cancellationToken);
        return returnedCity.ToDomain();
    }

    async Task<DomainCity> ICityRepository.UpdateOne(int cityId, DomainCity city)
    {
        var cityToUpdate = await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);

        CityMapper.ToInfrastructureUpdate(city, cityToUpdate);

        await _context.SaveChangesAsync(CancellationToken.None);

        return cityToUpdate.ToDomain();
    }
}