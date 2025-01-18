using System.Collections.ObjectModel;

namespace AccommodationBooking.Application.Common.Pagination;

public class PaginatedData<T>
{
    public int Total { get; init; }
    public IReadOnlyCollection<T> Data { get; init; }
}