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

    public Task<Review> GetOne(int id, CancellationToken cancellationToken)
    {
        return _reviewRepository.GetOne(id, cancellationToken);
    }

    public async Task<Review> InsertOne(Review review, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(review);
        var id = await _reviewRepository.InsertOne(review);
        return await GetOne(id, cancellationToken);
    }

    public async Task<PaginatedData<Review>> Search(
        int page,
        int pageSize,
        ReviewFilters filters,
        CancellationToken cancellationToken
        )
    {
        return await _reviewRepository.Search(page, pageSize, filters, cancellationToken);
    }

    public async Task<Review> UpdateOne(int id, Guid requesterId, Review review)
    {
        _validator.ValidateAndThrow(review);
        return await _reviewRepository.UpdateOne(id, requesterId, review);
    }
}