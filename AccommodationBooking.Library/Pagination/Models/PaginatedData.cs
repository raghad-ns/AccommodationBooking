namespace AccommodationBooking.Library.Pagination.Models;

public class PaginatedData<T>
{
    public int Total { get; init; }
    public IReadOnlyCollection<T> Data { get; init; }

    public delegate TTarget Mapper<TTarget>(T origin);

    public PaginatedData<TTarget> MapValues<TTarget>(Mapper<TTarget> mapper)
    {
        return new PaginatedData<TTarget>()
        {
            Total = this.Total,
            Data = this.Data.Select(mapper.Invoke).ToList(),
        };
    }
}