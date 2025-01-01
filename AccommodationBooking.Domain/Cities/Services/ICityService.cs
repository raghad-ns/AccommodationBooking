using AccommodationBooking.Domain.Cities.Models;

namespace AccommodationBooking.Domain.Cities.Services;

public interface ICityService
{
    Task<List<City>> GetCities();
    Task<City> GetCityById(int id);
    Task<City> GetCityByName(string name);
    Task CreateCity(City city);
    Task UpdateCity(int cityId, City city);
    Task DeleteCityById(int cityId);
    Task DeleteCityByName(string name);
}