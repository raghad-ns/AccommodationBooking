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
}
