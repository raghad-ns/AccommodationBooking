using AccommodationBooking.Application.Cities.Mappers;
using AccommodationBooking.Domain.Cities.Services;
using Microsoft.AspNetCore.Mvc;
using AccommodationBooking.Application.Cities.Models;

namespace AccommodationBooking.Application.Cities.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController: ControllerBase
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<ActionResult<List<City>>> GetCities(
        [FromQuery] int page=0, 
        [FromQuery] int pageSize=10,
        [FromQuery] int? id = null,
        [FromQuery] string? name = null,
        [FromQuery] string? country = null,
        [FromQuery] string? postOfficeCode = null
        )
    {
        var filters = new CityFilters
        {
            Id = id,
            Name = name,
            Country = country,
            PostOfficeCode = postOfficeCode
        };

        var cities = await _cityService.GetCities(page, pageSize, filters.ToDomain());

        return Ok(cities.Select(c => c.ToApplication()));
    }

    [HttpPost("add")]
    public async Task<ActionResult<City>> AddCity([FromBody] City city)
    {
        var createdCity = await _cityService.CreateCity(city.ToDomain());
        return Ok(createdCity);
    }

    [HttpPut("update")]
    public async Task<ActionResult<City>> UpdateCity([FromBody] City city)
    {
        var updatedCity = await _cityService.UpdateCity(city.Id,city.ToDomain());
        return Ok(updatedCity);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCity( int id)
    {
        await _cityService.DeleteCityById(id);
        //HttpContext.Session.SetInt32("StatusCode", 200);
        return Ok(new {Message= "Deleted successfully", StatusCode = 200 });
    }
}
