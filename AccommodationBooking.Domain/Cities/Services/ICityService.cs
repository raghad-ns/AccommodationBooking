using AccommodationBooking.Domain.Cities.Models;

namespace AccommodationBooking.Domain.Cities.Services;

public interface ICityService
{
    Task<List<City>> GetCities(int page, int pageSize, CityFilters cityFilters);
    Task<City> GetCityById(int id);
    Task<City> GetCityByName(string name);
    Task<City> CreateCity(City city);
    Task<City> UpdateCity(int cityId, City city);
    Task DeleteCityById(int cityId);
    Task DeleteCityByName(string name);
}