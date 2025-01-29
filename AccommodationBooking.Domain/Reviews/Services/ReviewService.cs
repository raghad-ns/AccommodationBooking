using AccommodationBooking.Domain.Reviews.Models;
using AccommodationBooking.Domain.Reviews.Repositories;
using AccommodationBooking.Library.Pagination.Models;

namespace AccommodationBooking.Domain.Reviews.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    async Task<Review> IReviewService.InsertOne(Review review, CancellationToken cancellationToken)
    {
        var id = await _reviewRepository.InsertOne(review);
        return await _reviewRepository.GetOne(id, cancellationToken);
    }

    async Task<PaginatedData<Review>> IReviewService.Search(
        int page,
        int pageSize,
        ReviewFilters filters,
        CancellationToken cancellationToken
        )
    {
        return await _reviewRepository.Search(page, pageSize, filters, cancellationToken);
    }

    async Task<Review> IReviewService.UpdateOne(int id, Review review)
    {
        return await _reviewRepository.UpdateOne(id, review);
    }
}