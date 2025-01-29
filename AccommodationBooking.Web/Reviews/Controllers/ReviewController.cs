using AccommodationBooking.Domain.Reviews.Services;
using AccommodationBooking.Library.Pagination.Models;
using AccommodationBooking.Web.Reviews.Mappers;
using AccommodationBooking.Web.Reviews.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Web.Reviews.Controllers;


[ApiController]
[Route("api/reviews")]
public class ReviewController: ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PaginatedData<Review>>> GetMany(
        CancellationToken cancellationToken,
        [FromQuery] ReviewFilters? filters,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 10
        )
    {
        var reviews = await _reviewService.Search(page, pageSize, filters.ToDomain(), cancellationToken);

        return Ok(new PaginatedData<Review>
        {
            Total = reviews.Total,
            Data = reviews.Data.Select(room => room.ToApplication()).ToList().AsReadOnly()
        });
    }

    [HttpPost]
    public async Task<ActionResult<Review>> CreateOne([FromBody] Review review, CancellationToken cancellationToken)
    {
        var createdRoom = await _reviewService.InsertOne(review.ToDomain(), cancellationToken);
        return Ok(createdRoom);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<Review>> UpdateOne([FromBody] Review review)
    {
        var updatedRoom = await _reviewService.UpdateOne(review.Id, review.ToDomain());
        return Ok(updatedRoom);
    }
}