using System.Collections.ObjectModel;

namespace AccommodationBooking.Application.Common.Pagination;

public class PaginatedData<T>
{
    public int Total { get; set; }
    public ReadOnlyCollection<T> Data { get; set; }
}