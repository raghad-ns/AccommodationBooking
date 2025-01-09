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

    Task<City> ICityService.CreateCity(City city)
    {
        return _cityRepository.CreateCity(city);
    }

    Task ICityService.DeleteCityById(int cityId)
    {
        return _cityRepository.DeleteCityById(cityId);
    }

    Task ICityService.DeleteCityByName(string name)
    {
        return _cityRepository.DeleteCityByName(name);
    }

    async Task<List<City>> ICityService.GetCities(int page, int pageSize, CityFilters cityFilters)
    {
        return await _cityRepository.GetCities(page, pageSize, cityFilters);
    }

    async Task<City> ICityService.GetCityById(int id)
    {
        return await _cityRepository.GetCityById(id);
    }

    async Task<City> ICityService.GetCityByName(string name)
    {
        return await _cityRepository.GetCityByName(name);
    }

    async Task<City> ICityService.UpdateCity(int cityId, City newCity)
    {
        return await _cityRepository.UpdateCity(cityId, newCity);
    }
}