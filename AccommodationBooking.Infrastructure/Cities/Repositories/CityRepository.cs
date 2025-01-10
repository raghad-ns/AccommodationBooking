using AccommodationBooking.Infrastructure.Cities.Mappers;
using DomainCity = AccommodationBooking.Domain.Cities.Models.City;
using DomainCityFilters = AccommodationBooking.Domain.Cities.Models.CityFilters;
using AccommodationBooking.Domain.Cities.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Cities.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AccommodationBookingContext _context;

    public CityRepository(AccommodationBookingContext context)
    {
        _context = context;
    }

    async Task<DomainCity> ICityRepository.CreateCity(DomainCity city)
    {
        var infraCity = city.ToInfrastructure();
        infraCity.CreatedAt = DateTime.UtcNow;
        infraCity.UpdatedAt = DateTime.UtcNow;

        _context.Cities.Add(infraCity);
        await _context.SaveChangesAsync();

        var createdCity = await _context.Cities.FirstOrDefaultAsync(c => c.Name.Equals(city.Name));
        return createdCity.ToDomain();
    }

    async Task ICityRepository.DeleteCityById(int cityId)
    {
        var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
        if (city != null)
        {
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }
    }

    Task ICityRepository.DeleteCityByName(string name)
    {
        throw new NotImplementedException();
    }

    Task<List<DomainCity>> ICityRepository.GetCities(int page, int pageSize, DomainCityFilters cityFilters)
    {
        return _context.Cities
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
            .ToListAsync();
    }

    async Task<DomainCity> ICityRepository.GetCityById(int id)
    {
        var returnedCity = await _context.Cities.FirstOrDefaultAsync(city => city.Id == id);
        return returnedCity.ToDomain();
    }

    async Task<DomainCity> ICityRepository.GetCityByName(string name)
    {
        var returnedCity = await _context.Cities.FirstOrDefaultAsync(city => city.Name.Equals(name));
        return returnedCity.ToDomain();
    }

    async Task<DomainCity> ICityRepository.UpdateCity(int cityId, DomainCity city)
    {
        var infraCity = city.ToInfrastructure();
        var cityToUpdate = await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);

        cityToUpdate.Name = city.Name;
        cityToUpdate.Country = city.Country;
        cityToUpdate.PostOfficeCode = city.PostOfficeCode;
        cityToUpdate.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        var updatedCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
        return updatedCity.ToDomain();
    }
}