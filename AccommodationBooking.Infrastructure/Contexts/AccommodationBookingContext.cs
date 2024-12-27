using AccommodationBooking.Infrastructure.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Contexts
{
    public class AccommodationBookingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AccommodationBookingContext(DbContextOptions options) : base(options)
        {
        }

    }
}
