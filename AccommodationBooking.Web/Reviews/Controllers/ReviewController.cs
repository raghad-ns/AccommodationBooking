using AccommodationBooking.Domain.Reviews.Services;
using AccommodationBooking.Library.Pagination.Models;
using AccommodationBooking.Web.Reviews.Mappers;
using AccommodationBooking.Web.Reviews.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace AccommodationBooking.Web.Reviews.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
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
            Data = reviews.Data.Select(review => review.ToApplication()).ToList().AsReadOnly()
        });
    }

    [HttpPost]
    public async Task<ActionResult<Review>> CreateOne([FromBody] Review review, CancellationToken cancellationToken)
    {
        var domainReview = review.ToDomain();

        if (Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
        {
            domainReview.UserId = userId;
            var createdReview = await _reviewService.InsertOne(domainReview, cancellationToken);
            return Ok(createdReview);
        }

        else return Unauthorized();
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<Review>> UpdateOne([FromBody] Review review)
    {
        if (Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
        {
            var updatedReview = await _reviewService.UpdateOne(review.Id, userId, review.ToDomain());
            return Ok(updatedReview);
        }
        return Forbid();
    }
}