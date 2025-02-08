using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Library.Pagination.Models;

namespace AccommodationBooking.Domain.Cities.Services;

public interface ICityService
{
    Task<PaginatedData<City>> Search(int page, int pageSize, CityFilters cityFilters, CancellationToken cancellationToken);
    Task<City> GetOne(int id, CancellationToken cancellationToken);
    Task<City> GetOneByName(string name, CancellationToken cancellationToken);
    Task<City> InsertOne(City city, CancellationToken cancellationToken);
    Task<City> UpdateOne(int cityId, City city);
    Task<int> DeleteOne(int cityId);
}