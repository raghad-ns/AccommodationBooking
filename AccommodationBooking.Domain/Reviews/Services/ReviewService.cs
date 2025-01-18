using AccommodationBooking.Domain.Common;
using AccommodationBooking.Domain.Reviews.Models;
using AccommodationBooking.Domain.Reviews.Repositories;

namespace AccommodationBooking.Domain.Reviews.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    async Task<Review> IReviewService.AddOne(Review review)
    {
        return await _reviewRepository.AddOne(review);
    }

    async Task<PaginatedData<Review>> IReviewService.Search(ReviewFilters filters)
    {
        return await _reviewRepository.Search(filters);
    }

    async Task<Review> IReviewService.UpdateOne(int id, Review review)
    {
        return await _reviewRepository.UpdateOne(id, review);
    }
}
