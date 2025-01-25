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

    async Task<City> ICityService.InsertOne(City city, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(city);
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
        _validator.ValidateAndThrow(newCity);
        return _cityRepository.UpdateOne(cityId, newCity);
    }
}