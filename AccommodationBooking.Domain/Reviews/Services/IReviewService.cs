using AccommodationBooking.Domain.Reviews.Models;
using AccommodationBooking.Library.Pagination.Models;

namespace AccommodationBooking.Domain.Reviews.Services;

public interface IReviewService
{
    Task<Review> AddOne(Review review);
    Task<Review> UpdateOne(int id, Review review);
    Task<PaginatedData<Review>> Search(ReviewFilters filters);
}
