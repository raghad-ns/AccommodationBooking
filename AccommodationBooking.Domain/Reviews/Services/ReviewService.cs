using AccommodationBooking.Domain.Exceptions.ClientError;
using AccommodationBooking.Domain.Reviews.Models;
using AccommodationBooking.Domain.Reviews.Repositories;
using AccommodationBooking.Library.Pagination.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AccommodationBooking.Domain.Reviews.Services;

public class ReviewService : IReviewService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReviewRepository _reviewRepository;
    private readonly IValidator<Review> _validator;

    public ReviewService(IReviewRepository reviewRepository, IValidator<Review> validator, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _reviewRepository = reviewRepository;
        _validator = validator;
    }

    public Task<Review> GetOne(int id, CancellationToken cancellationToken)
    {
        return _reviewRepository.GetOne(id, cancellationToken);
    }

    public async Task<Review> InsertOne(Review review, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        review.UserId = userId;

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

    public async Task<Review> UpdateOne(int id, Review review, CancellationToken cancellationToken)
    {
        var requesterId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        _validator.ValidateAndThrow(review);
        
        var reviewToUpdate = await _reviewRepository.GetOne(id, cancellationToken);
        if (reviewToUpdate != null)
        {
            if (reviewToUpdate.UserId != requesterId)
            {
                throw new UnauthorizedAccessException();
            }

            review.UserId = requesterId;
            return await _reviewRepository.UpdateOne(id, review);
        }
        else throw new RecordNotFoundException<int>(nameof(Review), nameof(Review.Id), id);
    }
}