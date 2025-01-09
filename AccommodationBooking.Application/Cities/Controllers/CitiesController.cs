using AccommodationBooking.Application.Cities.Mappers;
using DoaminCity = AccommodationBooking.Domain.Cities.Models.City;
using DoaminCityFilters = AccommodationBooking.Domain.Cities.Models.CityFilters;
using AccommodationBooking.Domain.Cities.Services;
using Microsoft.AspNetCore.Mvc;
using AccommodationBooking.Application.Cities.Models;

namespace AccommodationBooking.Application.Cities.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController: ControllerBase
{
    private readonly ICityService _cityService;
    private readonly CityMapper _mapper;

    public CitiesController(ICityService cityService, CityMapper mapper)
    {
        _cityService = cityService;
        _mapper = mapper;
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

        var cities = await _cityService.GetCities(page, pageSize, _mapper.ToDomain(filters));

        return Ok(cities.Select(c => _mapper.ToApplication(c)));
    }

    [HttpPost("add")]
    public async Task<ActionResult<City>> AddCity([FromBody] City city)
    {
        var createdCity = await _cityService.CreateCity(_mapper.ToDomain(city));
        return Ok(createdCity);
    }

    [HttpPut("update")]
    public async Task<ActionResult<City>> UpdateCity([FromBody] City city)
    {
        var updatedCity = await _cityService.UpdateCity(city.Id,_mapper.ToDomain(city));
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
