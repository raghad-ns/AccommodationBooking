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

    Task ICityRepository.CreateCity(Domain.Cities.Models.City city)
    {
        throw new NotImplementedException();
    }

    Task ICityRepository.DeleteCityById(int cityId)
    {
        throw new NotImplementedException();
    }

    Task ICityRepository.DeleteCityByName(string name)
    {
        throw new NotImplementedException();
    }

    Task<List<Domain.Cities.Models.City>> ICityRepository.GetCities()
    {
        return _context.Cities.Select(city => _mapper.ToDomainCity(city)).ToListAsync();
    }

    async Task<Domain.Cities.Models.City> ICityRepository.GetCityById(int id)
    {
        var returnedCity = await _context.Cities.FirstOrDefaultAsync(city => city.Id == id);
        return _mapper.ToDomainCity(returnedCity);
    }

    async Task<Domain.Cities.Models.City> ICityRepository.GetCityByName(string name)
    {
        var returnedCity = await _context.Cities.FirstOrDefaultAsync(city => city.Name.Equals(name));
        return _mapper.ToDomainCity(returnedCity);
    }

    Task ICityRepository.UpdateCity(int cityId, Domain.Cities.Models.City city)
    {
        throw new NotImplementedException();
    }
}