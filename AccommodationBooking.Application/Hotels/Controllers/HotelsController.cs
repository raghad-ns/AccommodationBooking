using AccommodationBooking.Application.Common.Pagination;
using AccommodationBooking.Application.Hotels.Mappers;
using AccommodationBooking.Application.Hotels.Models;
using AccommodationBooking.Domain.Hotels.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Application.Hotels.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PaginatedData<Hotel>>> GetMany(
        CancellationToken cancellationToken,
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

        var hotels = await _hotelService.Search(page, pageSize, filters.ToDomain(), cancellationToken);

        return Ok(new PaginatedData<Hotel>
        {
            Total = hotels.Total,
            Data = hotels.Data.Select(hotel => hotel.ToApplication()).ToList().AsReadOnly()
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Hotel>> CreateOne([FromBody] Hotel hotel, CancellationToken cancellationToken)
    {
        var createdHotel = await _hotelService.InsertOne(hotel.ToDomain(), cancellationToken);
        return Ok(createdHotel.ToApplication());
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<Hotel>> UpdateOne([FromBody] Hotel hotel)
    {
        var updatedHotel = await _hotelService.UpdateOne(hotel.Id, hotel.ToDomain());
        return Ok(updatedHotel.ToApplication());
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOne(int id)
    {
        await _hotelService.DeleteOne(id);
        return NoContent();
    }
}