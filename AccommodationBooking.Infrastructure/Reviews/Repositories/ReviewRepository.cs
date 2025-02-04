using AccommodationBooking.Domain.Exceptions.ClientError;
using AccommodationBooking.Domain.Reviews.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Reviews.Mappers;
using AccommodationBooking.Infrastructure.Reviews.Models;
using AccommodationBooking.Library.Pagination.Models;
using Microsoft.EntityFrameworkCore;
using DomainFilters = AccommodationBooking.Domain.Reviews.Models.ReviewFilters;
using DomainReview = AccommodationBooking.Domain.Reviews.Models.Review;

namespace AccommodationBooking.Infrastructure.Reviews.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AccommodationBookingContext _context;

    public ReviewRepository(AccommodationBookingContext context)
    {
        _context = context;
    }

    async public Task<DomainReview> GetOne(int id, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        return review.ToDomain();
    }

    async Task<int> IReviewRepository.InsertOne(DomainReview review)
    {
        var infraReview = review.ToInfrastructure();
        var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == review.HotelId);
        infraReview.Hotel = hotel;

        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == review.RoomId);
        infraReview.Room = room;

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == review.UserId);
        infraReview.User = user;

        _context.Reviews.Add(infraReview);
        await _context.SaveChangesAsync(CancellationToken.None);

        return infraReview.Id;
    }

    async Task<PaginatedData<DomainReview>> IReviewRepository.Search(
        int page,
        int pageSize,
        DomainFilters filters,
        CancellationToken cancellationToken
        )
    {
        IQueryable<Review> baseQuery = _context.Reviews;
        baseQuery = ApplySearchFilters(baseQuery, filters);

        var total = -1;
        if (page == 1) total = baseQuery.Count();

        var reviews = await baseQuery 
        .Skip(pageSize * (page - 1))
        .Take(pageSize)
        .Select(review => review.ToDomain())
        .ToListAsync(cancellationToken);

        return new PaginatedData<DomainReview>
        {
            Total = total,
            Data = reviews.AsReadOnly()
        };
    }

    private IQueryable<Review> ApplySearchFilters(IQueryable<Review> baseQuery, DomainFilters reviewFilters)
    {
        if (reviewFilters.Id != null)
            baseQuery = baseQuery.Where(r => r.Id == reviewFilters.Id);
        if (reviewFilters.UserId != null)
            baseQuery = baseQuery.Where(r => r.UserId == reviewFilters.UserId);
        if (reviewFilters.RoomId != null)
            baseQuery = baseQuery.Where(r => r.RoomId == reviewFilters.RoomId);
        if (reviewFilters.HotelId != null)
            baseQuery = baseQuery.Where(r => r.HotelId == reviewFilters.HotelId);
        if (reviewFilters.StartRatingFrom != null)
            baseQuery = baseQuery.Where(r => r.StarRating >= reviewFilters.StartRatingFrom);
        if (reviewFilters.StarRatingTo != null)
            baseQuery = baseQuery.Where(r => r.StarRating <= reviewFilters.StarRatingTo);
        if (reviewFilters.Comment != null)
            baseQuery = baseQuery.Where(r => r.Comment.ToLower().Contains(reviewFilters.Comment.ToLower()));

        return baseQuery;
    }

    async Task<DomainReview> IReviewRepository.UpdateOne(int id,Guid requesterId, DomainReview review)
    {
        var reviewToUpdate = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        if (reviewToUpdate != null)
        {
            if (reviewToUpdate.UserId != requesterId)
            {
                throw new UnauthorizedAccessException();
            }

            ReviewsMapper.ToInfrastructureUpdate(review, reviewToUpdate);

            await _context.SaveChangesAsync(CancellationToken.None);

            return reviewToUpdate.ToDomain();
        }
        else throw new RecordNotFoundException<int>(nameof(Review), nameof(Review.Id), id);
    }
}