using AccommodationBooking.Application.Rooms.Mappers;
using AccommodationBooking.Application.Rooms.Models;
using AccommodationBooking.Domain.Rooms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Application.Rooms.Controllers;


[ApiController]
[Route("api/rooms")]
public class RoomController: ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<Room>>> GetRooms(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? id = null,
        [FromQuery] string? roomNo = null,
        [FromQuery] string? hotelName = null,
        [FromQuery] string? description = null,
        [FromQuery] bool? isAvailable = null,
        [FromQuery] double? adultsCapacityFrom = null,
        [FromQuery] double? adultsCapacityTo = null,
        [FromQuery] double? ChildrenCapacityFrom = null,
        [FromQuery] double? childrenCapacityTo = null
        )
    {
        var filters = new RoomFilters
        {
            Id = id,
            RoomNo = roomNo,
            HotelName = hotelName,
            Description = description,
            IsAvailable = isAvailable,
            AdultsCapacityFrom = adultsCapacityFrom,
            AdultsCapacityTo= adultsCapacityTo,
            ChildrenCapacityFrom = ChildrenCapacityFrom,
            ChildrenCapacityTo = childrenCapacityTo
        };

        var rooms = await _roomService.Search(page, pageSize, filters.ToDomain());

        return Ok(rooms.Select(c => c.ToApplication()));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("add")]
    public async Task<ActionResult<Room>> AddCity([FromBody] Room room)
    {
        var createdRoom = await _roomService.AddOne(room.ToDomain());
        return Ok(createdRoom);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update")]
    public async Task<ActionResult<Room>> UpdateCity([FromBody] Room room)
    {
        var updatedRoom = await _roomService.UpdateOne(room.Id, room.ToDomain());
        return Ok(updatedRoom);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRoom(int id)
    {
        await _roomService.DeleteOne(id);
        return Ok(new { Message = "Deleted successfully", StatusCode = 200 });
    }
}