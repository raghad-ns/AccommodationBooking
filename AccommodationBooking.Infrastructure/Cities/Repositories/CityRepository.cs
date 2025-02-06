using AccommodationBooking.Domain.Cities.Repositories;
using AccommodationBooking.Infrastructure.Cities.Mappers;
using AccommodationBooking.Infrastructure.Cities.Models;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Library.Pagination.Models;
using Microsoft.EntityFrameworkCore;
using DomainCity = AccommodationBooking.Domain.Cities.Models.City;
using DomainCityFilters = AccommodationBooking.Domain.Cities.Models.CityFilters;

namespace AccommodationBooking.Infrastructure.Cities.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AccommodationBookingContext _context;

    public CityRepository(AccommodationBookingContext context)
    {
        _context = context;
    }

    async Task<int> ICityRepository.InsertOne(DomainCity city)
    {
        var infraCity = city.ToInfrastructure();

        _context.Cities.Add(infraCity);
        await _context.SaveChangesAsync(CancellationToken.None);

        return infraCity.Id;
    }

    async Task ICityRepository.DeleteOne(int cityId)
    {
        await _context.Cities.Where(c => c.Id == cityId).ExecuteDeleteAsync();
    }

    async Task<PaginatedData<DomainCity>> ICityRepository.Search(
        int page,
        int pageSize,
        DomainCityFilters cityFilters,
        CancellationToken cancellationToken
        )
    {
        IQueryable<City> baseQuery = _context.Cities;
        baseQuery = ApplySearchFilters(baseQuery, cityFilters);

        var total = -1;
        if (page == 1) total = baseQuery.Count();

        var cities = await baseQuery
            .Paginate<City>(page, pageSize)
            .Include(c => c.Hotels)
            .Select(city => city.ToDomain())
            .ToListAsync(cancellationToken);

        return new PaginatedData<DomainCity>
        {
            Total = total,
            Data = cities.AsReadOnly()
        };
    }

    private IQueryable<City> ApplySearchFilters(IQueryable<City> baseQuery, DomainCityFilters cityFilters)
    {
        if (cityFilters.Id is not null)
            baseQuery = baseQuery.Where(city => city.Id == cityFilters.Id);

        if (cityFilters.Name is not null)
            baseQuery = baseQuery.Where(city => city.Name.ToLower().Equals(cityFilters.Name.ToLower()));

        if (cityFilters.Country is not null)
            baseQuery = baseQuery.Where(city => city.Country.ToLower().Equals(cityFilters.Country.ToLower()));

        if (cityFilters.PostOfficeCode is not null)
            baseQuery = baseQuery.Where(city => city.PostOfficeCode.ToLower().Equals(cityFilters.PostOfficeCode.ToLower()));

        return baseQuery;
    }

    async Task<DomainCity> ICityRepository.GetOne(int id, CancellationToken cancellationToken)
    {
        var returnedCity = await _context.Cities.FirstOrDefaultAsync(city => city.Id == id, cancellationToken);
        return returnedCity.ToDomain();
    }

    async Task<DomainCity> ICityRepository.GetOneByName(string name, CancellationToken cancellationToken)
    {
        var returnedCity = await _context.Cities.FirstOrDefaultAsync(city => city.Name.Equals(name), cancellationToken);
        return returnedCity.ToDomain();
    }

    async Task<DomainCity> ICityRepository.UpdateOne(int cityId, DomainCity city)
    {
        var cityToUpdate = await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);

        CityMapper.ToInfrastructureUpdate(city, cityToUpdate);

        await _context.SaveChangesAsync(CancellationToken.None);

        return cityToUpdate.ToDomain();
    }
}