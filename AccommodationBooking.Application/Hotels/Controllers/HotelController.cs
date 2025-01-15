﻿using AccommodationBooking.Application.Common.Pagination;
using AccommodationBooking.Application.Hotels.Mappers;
using AccommodationBooking.Application.Hotels.Models;
using AccommodationBooking.Application.Rooms.Mappers;
using AccommodationBooking.Application.Rooms.Models;
using AccommodationBooking.Domain.Hotels.Services;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PaginatedData<Hotel>>> GetCities(
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

        return Ok(new PaginatedData<Hotel>
        {
            Total = hotels.Total,
            Data = hotels.Data.Select(hotel => hotel.ToApplication()).ToList().AsReadOnly()
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("add")]
    public async Task<ActionResult<Hotel>> AddCity([FromBody] Hotel hotel)
    {
        var createdHotel = await _hotelService.AddOne(hotel.ToDomain());
        return Ok(createdHotel.ToApplication());
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update")]
    public async Task<ActionResult<Hotel>> UpdateHotel([FromBody] Hotel hotel)
    {
        var updatedHotel = await _hotelService.UpdateOne(hotel.Id, hotel.ToDomain());
        return Ok(updatedHotel.ToApplication());
    }

    [Authorize()]
    [HttpGet("{id}")]
    public async Task<ActionResult<Hotel>> GetHotel(int id)
    {
        var hotel = await _hotelService.GetOne(id);
        return Ok(hotel);
    }

    [Authorize()]
    [HttpGet("{hotelId}/rooms")]
    public async Task<ActionResult<List<Room>>> GetRooms(int hotelId)
    {
        var rooms = await _hotelService.GetRooms(hotelId);
        return Ok(rooms.Select(r => r.ToApplication()));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHotel(int id)
    {
        await _hotelService.DeleteOne(id);
        return Ok(new { Message = "Deleted successfully", StatusCode = 200 });
    }
}