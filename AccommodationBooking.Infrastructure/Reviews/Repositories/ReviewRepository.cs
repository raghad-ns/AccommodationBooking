﻿using DomainFilters = AccommodationBooking.Domain.Reviews.Models.ReviewFilters;
using AccommodationBooking.Domain.Reviews.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Reviews.Mappers;
using DomainReview = AccommodationBooking.Domain.Reviews.Models.Review;
using Microsoft.EntityFrameworkCore;
using AccommodationBooking.Library.Pagination.Models;
using AccommodationBooking.Domain.Exceptions.ClientError;
using AccommodationBooking.Library.Exceptions;
using AccommodationBooking.Infrastructure.Reviews.Models;

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

    async Task<int> IReviewRepository.AddOne(DomainReview review)
    {
        var infraReview = review.ToInfrastructure();
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

        var reviews = await baseQuery 
        .Skip(pageSize * page)
        .Take(pageSize)
        .Select(review => review.ToDomain())
        .ToListAsync(cancellationToken);

        return new PaginatedData<DomainReview>
        {
            Total = _context.Reviews.Count(),
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

    async Task<DomainReview> IReviewRepository.UpdateOne(int id, DomainReview review)
    {
        var reviewToUpdate = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        if (reviewToUpdate != null)
        {
            ReviewsMapper.ToInfrastructureUpdate(review, reviewToUpdate);

            await _context.SaveChangesAsync(CancellationToken.None);

            return reviewToUpdate.ToDomain();
        }
        else throw new UserError(ExceptionMessage.ReviewDoesNotExist);
    }
}