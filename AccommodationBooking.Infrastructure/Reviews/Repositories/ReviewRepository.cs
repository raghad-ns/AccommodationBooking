using AccommodationBooking.Domain.Common;
using AccommodationBooking.Domain.Reviews.Models;
using AccommodationBooking.Domain.Reviews.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Reviews.Mappers;
using DomainReview = AccommodationBooking.Domain.Reviews.Models.Review;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Reviews.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AccommodationBookingContext _context;

    public ReviewRepository(AccommodationBookingContext context)
    {
        _context = context;
    }
    async Task<Review> IReviewRepository.AddOne(Review review)
    {
        _context.Reviews.Add(review.ToInfrastructure());
        await _context.SaveChangesAsync(new CancellationToken());

        var addedReview = await _context.Reviews.OrderByDescending(r => r.CreatedAt).FirstOrDefaultAsync();

        return addedReview.ToDomain();
    }

    async Task<PaginatedData<DomainReview>> IReviewRepository.Search(ReviewFilters filters)
    {
        var reviews = await _context.Reviews
        .Where(review => (
            (filters.Id != null ? review.Id == filters.Id : true) &&
            (filters.UserId != null ? review.UserId == filters.UserId : true) &&
            (filters.RoomId != null ? review.RoomId == filters.RoomId : true) &&
            (filters.HotelId != null ? review.HotelId == filters.HotelId : true) &&
            (filters.StartRatingFrom != null ? review.StarRating >= filters.StartRatingFrom : true) &&
            (filters.StarRatingTo != null ? review.StarRating <= filters.StarRatingTo : true) &&
            (filters.Comment != null ? review.Comment.ToLower().Contains(filters.Comment.ToLower()) : true) 
            ))
            .Select(review => review.ToDomain())
            .ToListAsync();

        return new PaginatedData<DomainReview>
        {
            Total = _context.Reviews.Count(),
            Data = reviews.AsReadOnly()
        };
    }

    Task<Review> IReviewRepository.UpdateOne(int id, Review review)
    {
        throw new NotImplementedException();
    }
}