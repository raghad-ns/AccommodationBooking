using System.Collections.ObjectModel;

namespace AccommodationBooking.Domain.Common;

public class PaginatedData<T>
{
    public int Total { get; set; }
    public ReadOnlyCollection<T> Data { get; set; }
}