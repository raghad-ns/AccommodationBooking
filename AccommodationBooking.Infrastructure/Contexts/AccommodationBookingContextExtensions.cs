using Microsoft.EntityFrameworkCore.Storage;

namespace AccommodationBooking.Infrastructure.Contexts;

public static class AccommodationBookingContextExtensions
{
    public static async Task<TModel> PerformTransaction<TModel>(this AccommodationBookingContext context, Func<IDbContextTransaction, Task<TModel>> callback)
    {
        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            return await callback(transaction);

        } catch(Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}