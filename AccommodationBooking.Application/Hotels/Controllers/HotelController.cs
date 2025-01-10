using AccommodationBooking.Application.Hotels.Mappers;
using AccommodationBooking.Application.Hotels.Models;
using AccommodationBooking.Domain.Hotels.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Application.Hotels.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Hotel>>> GetCities(
        [FromQuery] int page=0, 
        [FromQuery] int pageSize=10, 
        [FromQuery] int? id=null,
        [FromQuery] string? name = null,
        [FromQuery] string? description = null,
        [FromQuery] string? address = null,
        [FromQuery] string? cityName = null,
        [FromQuery] double? starRatingGreaterThanOrEqual = null,
        [FromQuery] double? starRatingLessThanOrEqual = null,
        [FromQuery] List<string> amenities= null
        )
    {
        var filters = new HotelFilters
        {
            Id = id,
            Name = name,
            Description = description,
            Address = address,
            Amenities = amenities,
            StarRatingGreaterThanOrEqual = starRatingGreaterThanOrEqual,
            StartRatingLessThanOrEqual = starRatingLessThanOrEqual,
            City = cityName
        };

        var hotels = await _hotelService.Search(page, pageSize, filters.ToDomain());
        
        return Ok(hotels.Select(hotel => hotel.ToApplication()));
    }

    [HttpPost("add")]
    public async Task<ActionResult<Hotel>> AddCity([FromBody] Hotel hotel)
    {
        var createdHotel = await _hotelService.AddOne(hotel.ToDomain());
        return Ok(createdHotel.ToApplication());
    }

    [HttpPut("update")]
    public async Task<ActionResult<Hotel>> UpdateHotel([FromBody] Hotel hotel)
    {
        var updatedHotel = await _hotelService.UpdateOne(hotel.Id, hotel.ToDomain());
        return Ok(updatedHotel.ToApplication());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHotel(int id)
    {
        await _hotelService.DeleteOne(id);
        return Ok(new { Message = "Deleted successfully", StatusCode = 200 });
    }
}