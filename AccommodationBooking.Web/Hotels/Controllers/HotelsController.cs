using AccommodationBooking.Domain.Hotels.Services;
using AccommodationBooking.Library.Pagination.Models;
using AccommodationBooking.Web.Hotels.Mappers;
using AccommodationBooking.Web.Hotels.Models;
using AccommodationBooking.Web.Rooms.Mappers;
using AccommodationBooking.Web.Rooms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Web.Hotels.Controllers;

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
    public async Task<ActionResult<PaginatedData<Hotel>>> Search(
        CancellationToken cancellationToken,
        [FromQuery] HotelFilters? filters,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
        )
    {
        var hotels = await _hotelService.Search(page, pageSize, filters.ToDomain(), cancellationToken);

        return Ok(hotels.MapValues<Hotel>(hotel => hotel.ToApplication()));
    }

    [Authorize()]
    [HttpGet("{id}")]
    public async Task<ActionResult<Hotel>> GetHotel(int id, CancellationToken cancellationToken)
    {
        var hotel = await _hotelService.GetOne(id, cancellationToken);
        return Ok(hotel);
    }

    [Authorize()]
    [HttpGet("{hotelId}/rooms")]
    public async Task<ActionResult<IReadOnlyCollection<Room>>> GetRooms(int hotelId, CancellationToken cancellationToken)
    {
        var rooms = await _hotelService.GetRooms(hotelId, cancellationToken);
        return Ok(rooms.Select(r => r.ToApplication()));
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