namespace AccommodationBooking.Library.Pagination.Models;

public class PaginatedData<T>
{
    public int Total { get; init; }
    public IReadOnlyCollection<T> Data { get; init; }
}