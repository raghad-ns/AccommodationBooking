using AccommodationBooking.Domain.Reviews.Services;
using AccommodationBooking.Library.Pagination.Models;
using AccommodationBooking.Web.Reviews.Mappers;
using AccommodationBooking.Web.Reviews.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<PaginatedData<Review>>> Search(
        CancellationToken cancellationToken,
        [FromQuery] ReviewFilters? filters,
        [FromQuery] int page = 1,
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

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Review>> CreateOne([FromBody] Review review, CancellationToken cancellationToken)
    {
        var domainReview = review.ToDomain();
        var createdReview = await _reviewService.InsertOne(domainReview, cancellationToken);
        return Ok(createdReview);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<Review>> UpdateOne([FromBody] Review review, CancellationToken cancellationToken)
    {
        var updatedReview = await _reviewService.UpdateOne(review.Id, review.ToDomain(), cancellationToken);
        return Ok(updatedReview);
    }
}