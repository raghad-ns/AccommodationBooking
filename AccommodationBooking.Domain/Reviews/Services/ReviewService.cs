using AccommodationBooking.Domain.Reviews.Models;
using AccommodationBooking.Domain.Reviews.Repositories;
using AccommodationBooking.Library.Pagination.Models;
using FluentValidation;

namespace AccommodationBooking.Domain.Reviews.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IValidator<Review> _validator;

    public ReviewService(IReviewRepository reviewRepository, IValidator<Review> validator)
    {
        _reviewRepository = reviewRepository;
        _validator = validator;
    }
    async Task<Review> IReviewService.InsertOne(Review review, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(review);
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

    async Task<Review> IReviewService.UpdateOne(int id, Guid requesterId, Review review)
    {
        _validator.ValidateAndThrow(review);
        return await _reviewRepository.UpdateOne(id, requesterId, review);
    }
}