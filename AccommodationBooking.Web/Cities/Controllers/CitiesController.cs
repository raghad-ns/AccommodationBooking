using AccommodationBooking.Domain.Cities.Services;
using AccommodationBooking.Library.Pagination.Models;
using AccommodationBooking.Web.Cities.Mappers;
using AccommodationBooking.Web.Cities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Web.Cities.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PaginatedData<City>>> Search(
        CancellationToken cancellationToken,
        [FromQuery] CityFilters? filters,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
        )
    {
        var cities = await _cityService.Search(page, pageSize, filters.ToDomain(), cancellationToken);

        return Ok(new PaginatedData<City>
        {
            Total = cities.Total,
            Data = cities.Data.Select(city => city.ToApplication()).ToList().AsReadOnly()
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<City>> CreateOne([FromBody] City city, CancellationToken cancellationToken)
    {
        var createdCity = await _cityService.InsertOne(city.ToDomain(), cancellationToken);
        return Ok(createdCity);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<City>> UpdateOne([FromBody] City city)
    {
        var updatedCity = await _cityService.UpdateOne(city.Id, city.ToDomain());
        return Ok(updatedCity);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOne(int id)
    {
        await _cityService.DeleteOne(id);
        return NoContent();
    }
}