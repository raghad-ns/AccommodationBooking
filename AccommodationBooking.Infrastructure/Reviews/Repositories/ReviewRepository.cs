using AccommodationBooking.Domain.Reviews.Models;
using AccommodationBooking.Domain.Reviews.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Reviews.Mappers;
using DomainReview = AccommodationBooking.Domain.Reviews.Models.Review;
using Microsoft.EntityFrameworkCore;
using AccommodationBooking.Library.Pagination.Models;

namespace AccommodationBooking.Infrastructure.Reviews.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AccommodationBookingContext _context;

    public ReviewRepository(AccommodationBookingContext context)
    {
        _context = context;
    }

    async public Task<DomainReview> GetOne(int id)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        return review.ToDomain();
    }

    async Task<int> IReviewRepository.AddOne(Review review)
    {
        var infraReview = review.ToInfrastructure();
        _context.Reviews.Add(infraReview);
        await _context.SaveChangesAsync(new CancellationToken());

        return infraReview.Id;
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