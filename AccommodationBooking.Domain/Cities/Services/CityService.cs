using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Cities.Repositories;

namespace AccommodationBooking.Domain.Cities.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    Task ICityService.CreateCity(City city)
    {
        throw new NotImplementedException();
    }

    Task ICityService.DeleteCityById(int cityId)
    {
        throw new NotImplementedException();
    }

    Task ICityService.DeleteCityByName(string name)
    {
        throw new NotImplementedException();
    }

    async Task<List<City>> ICityService.GetCities()
    {
        return await _cityRepository.GetCities();
    }

    async Task<City> ICityService.GetCityById(int id)
    {
        return await _cityRepository.GetCityById(id);
    }

    async Task<City> ICityService.GetCityByName(string name)
    {
        return await _cityRepository.GetCityByName(name);
    }

    Task ICityService.UpdateCity(int cityId, City newCity)
    {
        throw new NotImplementedException();
    }
}