using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Cities.Repositories;
using AccommodationBooking.Library.Pagination.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.Cities.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly IValidator<City> _validator;

    public CityService(ICityRepository cityRepository, IValidator<City> validator)
    {
        _cityRepository = cityRepository;
        _validator = validator;
    }

    public async Task<City> InsertOne(City city, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(city);
        var id = await _cityRepository.InsertOne(city);
        return await GetOne(id, cancellationToken);
    }

    public Task<int> DeleteOne(int cityId)
    {
        return _cityRepository.DeleteOne(cityId);
    }

    public Task<PaginatedData<City>> Search(int page, int pageSize, CityFilters cityFilters, CancellationToken cancellationToken)
    {
        return _cityRepository.Search(page, pageSize, cityFilters, cancellationToken);
    }

    public Task<City> GetOne(int id, CancellationToken cancellationToken)
    {
        return _cityRepository.GetOne(id, cancellationToken);
    }

    public Task<City> GetOneByName(string name, CancellationToken cancellationToken)
    {
        return _cityRepository.GetOneByName(name, cancellationToken);
    }

    public Task<City> UpdateOne(int cityId, City newCity)
    {
        _validator.ValidateAndThrow(newCity);
        return _cityRepository.UpdateOne(cityId, newCity);
    }
}