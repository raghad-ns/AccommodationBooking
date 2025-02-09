using Microsoft.EntityFrameworkCore.Storage;

namespace AccommodationBooking.Infrastructure.Contexts;

public static class AccommodationBookingContextExtensions
{
    public delegate Task<TModel> Transaction<TModel>(IDbContextTransaction transaction);

    public static async Task<TModel> PerformTransaction<TModel>(
        this AccommodationBookingContext context,
        Transaction<TModel> callback
        )
    {
        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            return await callback(transaction);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> baseQuery, int page, int pageSize)
    => baseQuery
      .Skip((page - 1) * pageSize)
      .Take(pageSize);
}