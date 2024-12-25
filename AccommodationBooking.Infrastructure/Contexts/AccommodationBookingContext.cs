using AccommodationBooking.Domain.User.Models;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Contexts
{
    public class AccommodationBookingContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public AccommodationBookingContext(DbContextOptions options) : base(options)
        {
        }

    }
}
