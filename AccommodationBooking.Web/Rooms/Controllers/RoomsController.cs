﻿using AccommodationBooking.Application.Rooms.Models;
using AccommodationBooking.Domain.Rooms.Services;
using AccommodationBooking.Library.Pagination.Models;
using AccommodationBooking.Web.Rooms.Mappers;
using AccommodationBooking.Web.Rooms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Web.Rooms.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PaginatedData<Room>>> Search(
        CancellationToken cancellationToken,
        [FromQuery] RoomFilters? filters,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
        )
    {
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