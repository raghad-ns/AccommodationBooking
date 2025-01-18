using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Cities.Repositories;
using AccommodationBooking.Domain.Common;

namespace AccommodationBooking.Domain.Cities.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    async Task<City> ICityService.InsertOne(City city, CancellationToken cancellationToken)
    {
        var id = await _cityRepository.InsertOne(city);
        return await _cityRepository.GetOne(id, cancellationToken);
    }

    Task ICityService.DeleteOne(int cityId)
    {
        return _cityRepository.DeleteOne(cityId);
    }

    Task<PaginatedData<City>> ICityService.Search(int page, int pageSize, CityFilters cityFilters, CancellationToken cancellationToken)
    {
        return _cityRepository.Search(page, pageSize, cityFilters, cancellationToken);
    }

    Task<City> ICityService.GetOne(int id, CancellationToken cancellationToken)
    {
        return _cityRepository.GetOne(id, cancellationToken);
    }

    Task<City> ICityService.GetOneByName(string name, CancellationToken cancellationToken)
    {
        return _cityRepository.GetOneByName(name, cancellationToken);
    }

    Task<City> ICityService.UpdateOne(int cityId, City newCity)
    {
        return _cityRepository.UpdateOne(cityId, newCity);
    }
}