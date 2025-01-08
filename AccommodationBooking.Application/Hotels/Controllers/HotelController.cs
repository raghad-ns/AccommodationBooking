using AccommodationBooking.Domain.Hotels.Models;
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
    public async Task<ActionResult<List<Hotel>>> GetCities()
    {
        var hotels = await _hotelService.GetHotels();
        //return Ok(hotels);
        return Ok(hotels);
    }

    [HttpPost("add")]
    public async Task<ActionResult<Hotel>> AddCity([FromBody] Hotel hotel)
    {
        var createdHotel = await _hotelService.CreateHotel(hotel);
        return Ok(createdHotel);
    }

    [HttpPut("update")]
    public async Task<ActionResult<Hotel>> UpdateHotel([FromBody] Hotel hotel)
    {
        var updatedHotel = await _hotelService.UpdateHotel(hotel.Id, hotel);
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
