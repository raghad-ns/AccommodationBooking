using AccommodationBooking.Library.Pagination.Models;
using AccommodationBooking.Application.Rooms.Mappers;
using AccommodationBooking.Application.Rooms.Models;
using AccommodationBooking.Domain.Rooms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Application.Rooms.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomsController: ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PaginatedData<Room>>> GetMany(
        CancellationToken cancellationToken,
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

        var rooms = await _roomService.Search(page, pageSize, filters.ToDomain(), cancellationToken);

        return Ok(new PaginatedData<Room>
        {
            Total = rooms.Total,
            Data = rooms.Data.Select(room => room.ToApplication()).ToList().AsReadOnly()
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Room>> CreateOne([FromBody] Room room, CancellationToken cancellationToken)
    {
        var createdRoom = await _roomService.InsertOne(room.ToDomain(), cancellationToken);
        return Ok(createdRoom);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<Room>> UpdateOne([FromBody] Room room)
    {
        var updatedRoom = await _roomService.UpdateOne(room.Id, room.ToDomain());
        return Ok(updatedRoom);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOne(int id)
    {
        await _roomService.DeleteOne(id);
        return NoContent();
    }
}