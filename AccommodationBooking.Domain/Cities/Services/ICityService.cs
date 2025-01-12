using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Common;

namespace AccommodationBooking.Domain.Cities.Services;

public interface ICityService
{
    Task<PaginatedData<City>> Search(int page, int pageSize, CityFilters cityFilters);
    Task<City> GetOne(int id);
    Task<City> GetOneByName(string name);
    Task<City> AddOne(City city);
    Task<City> UpdateOne(int cityId, City city);
    Task DeleteOne(int cityId);
}