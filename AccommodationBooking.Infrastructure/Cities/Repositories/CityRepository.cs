using AccommodationBooking.Infrastructure.Cities.Mappers;
using DomainCity = AccommodationBooking.Domain.Cities.Models.City;
using DomainCityFilters = AccommodationBooking.Domain.Cities.Models.CityFilters;
using AccommodationBooking.Domain.Cities.Repositories;
using AccommodationBooking.Infrastructure.Cities.Mappers;
using AccommodationBooking.Infrastructure.Cities.Models;
using AccommodationBooking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Cities.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AccommodationBookingContext _context;
    private readonly InfrastructureDomainCityMapper _mapper;

    public CityRepository(AccommodationBookingContext context, InfrastructureDomainCityMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    async Task<DomainCity> ICityRepository.CreateCity(DomainCity city)
    {
        var infraCity = _mapper.ToInfrastructureCity(city);
        infraCity.CreatedAt = DateTime.UtcNow;
        infraCity.UpdatedAt = DateTime.UtcNow;

        _context.Cities.Add(infraCity);
        await _context.SaveChangesAsync();

        var createdCity = await _context.Cities.FirstOrDefaultAsync(c => c.Name.Equals(city.Name));
        return _mapper.ToDomainCity(createdCity);
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
        var infraFilters = _mapper.ToInfrastructure(cityFilters);
        return _context.Cities
            .Where(c => (
                (infraFilters.Id != null ? c.Id == infraFilters.Id : true) &&
                (infraFilters.Name != null ? c.Name.ToLower().Contains(infraFilters.Name.ToLower()) : true) &&
                (infraFilters.Country != null ? c.Country.ToLower().Contains(infraFilters.Country.ToLower()) : true) &&
                (infraFilters.PostOfficeCode != null ? c.PostOfficeCode.ToLower().Contains(infraFilters.PostOfficeCode.ToLower()) : true)
            ))
            .Skip(page * pageSize)
            .Take(pageSize)
            .Include(c => c.Hotels)
            .Select(city => _mapper.ToDomainCityIncludeHotels(city))
            .ToListAsync();
    }

    async Task<DomainCity> ICityRepository.GetCityById(int id)
    {
        var returnedCity = await _context.Cities.FirstOrDefaultAsync(city => city.Id == id);
        return _mapper.ToDomainCity(returnedCity);
    }

    async Task<DomainCity> ICityRepository.GetCityByName(string name)
    {
        var returnedCity = await _context.Cities.FirstOrDefaultAsync(city => city.Name.Equals(name));
        return _mapper.ToDomainCity(returnedCity);
    }

    async Task<DomainCity> ICityRepository.UpdateCity(int cityId, DomainCity city)
    {
        var infraCity = _mapper.ToInfrastructureCity(city);
        var cityToUpdate = await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);

        cityToUpdate.Name = city.Name;
        cityToUpdate.Country = city.Country;
        cityToUpdate.PostOfficeCode = city.PostOfficeCode;
        cityToUpdate.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        var updatedCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
        return _mapper.ToDomainCity(updatedCity);
    }
}