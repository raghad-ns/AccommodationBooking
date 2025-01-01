using AccommodationBooking.Domain.Cities.Models;
using AccommodationBooking.Domain.Cities.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<City>>> GetCities()
    {
        var cities = await _cityService.GetCities();
        return Ok(cities);
    }

    [HttpPost("add")]
    public async Task<ActionResult<City>> AddCity([FromBody] City city)
    {
        var createdCity = await _cityService.CreateCity(city);
        return Ok(createdCity);
    }

    [HttpPut("update")]
    public async Task<ActionResult<City>> UpdateCity([FromBody] City city)
    {
        var updatedCity = await _cityService.UpdateCity(city.Id,city);
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
