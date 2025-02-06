using AccommodationBooking.Domain.Reviews.Models;
using AccommodationBooking.Library.Pagination.Models;

namespace AccommodationBooking.Domain.Reviews.Repositories;

public interface IReviewRepository
{
    Task<int> InsertOne(Review review);
    Task<Review> UpdateOne(int id, Review review);
    Task<PaginatedData<Review>> Search(int page, int pageSize, ReviewFilters filters, CancellationToken cancellationToken);
    Task<Review> GetOne(int id, CancellationToken cancellationToken);
}