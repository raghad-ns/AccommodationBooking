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

    Task<City> ICityService.AddOne(City city)
    {
        return _cityRepository.AddOne(city);
    }

    Task ICityService.DeleteOne(int cityId)
    {
        return _cityRepository.DeleteOne(cityId);
    }

    async Task<List<City>> ICityService.Search(int page, int pageSize, CityFilters cityFilters)
    {
        return await _cityRepository.Search(page, pageSize, cityFilters);
    }

    async Task<City> ICityService.GetOne(int id)
    {
        return await _cityRepository.GetOne(id);
    }

    async Task<City> ICityService.GetOneByName(string name)
    {
        return await _cityRepository.GetOneByName(name);
    }

    async Task<City> ICityService.UpdateOne(int cityId, City newCity)
    {
        return await _cityRepository.UpdateOne(cityId, newCity);
    }
}