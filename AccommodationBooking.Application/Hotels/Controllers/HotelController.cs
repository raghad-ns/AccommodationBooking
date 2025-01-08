//using AccommodationBooking.Domain.Hotels.Models;
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
    private readonly HotelMapper _mapper;

    public HotelController(IHotelService hotelService, HotelMapper hotelMapper)
    {
        _hotelService = hotelService;
        _mapper = hotelMapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Hotel>>> GetCities([FromQuery] int page=0, [FromQuery] int pageSize=10)
    {
        var hotels = await _hotelService.GetHotels(page, pageSize);
        
        return Ok(hotels.Select(hotel => _mapper.ToApplication(hotel)));
    }

    [HttpPost("add")]
    public async Task<ActionResult<Hotel>> AddCity([FromBody] Hotel hotel)
    {
        var createdHotel = await _hotelService.CreateHotel(_mapper.ToDomain(hotel));
        return Ok(createdHotel);
    }

    [HttpPut("update")]
    public async Task<ActionResult<Hotel>> UpdateHotel([FromBody] Hotel hotel)
    {
        var updatedHotel = await _hotelService.UpdateHotel(hotel.Id, _mapper.ToDomain(hotel));
        return Ok(updatedHotel);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHotel(int id)
    {
        await _hotelService.DeleteHotelById(id);
        //HttpContext.Session.SetInt32("StatusCode", 200);
        return Ok(new { Message = "Deleted successfully", StatusCode = 200 });
    }
}
