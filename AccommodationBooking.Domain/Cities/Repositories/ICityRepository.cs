using AccommodationBooking.Domain.Cities.Models;

namespace AccommodationBooking.Domain.Cities.Repositories;

public interface ICityRepository
{
    Task<List<City>> Search(int page, int pageSize, CityFilters cityFilters);
    Task<City> GetOne(int id);
    Task<City> GetOneByName(string name);
    Task<City> AddOne(City city);
    Task<City> UpdateOne(int cityId, City city);
    Task DeleteOne(int cityId);
}