using AccommodationBooking.Infrastructure.Cities.Models;
using AccommodationBooking.Infrastructure.Hotels.Models;
using AccommodationBooking.Infrastructure.Rooms.Models;
using AccommodationBooking.Infrastructure.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AccommodationBooking.Infrastructure.Contexts
{
    public class AccommodationBookingContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public AccommodationBookingContext(DbContextOptions<AccommodationBookingContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<City>(entity =>
            {
                entity
                .HasMany(city => city.Hotels)
                .WithOne(hotel => hotel.City)
                .HasForeignKey(hotel => hotel.CityId)
                .HasPrincipalKey(city => city.Id);
            });
            
            builder.Entity<Hotel>(entity =>
            {
                entity
                .HasMany(hotel => hotel.Rooms)
                .WithOne(room => room.Hotel)
                .HasForeignKey(room => room.HotelId)
                .HasPrincipalKey(hotel => hotel.Id);
            });
        }
    }
}