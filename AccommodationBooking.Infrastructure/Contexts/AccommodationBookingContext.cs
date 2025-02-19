﻿using AccommodationBooking.Infrastructure.Cities.Models;
using AccommodationBooking.Infrastructure.Hotels.Models;
using AccommodationBooking.Infrastructure.Reviews.Models;
using AccommodationBooking.Infrastructure.Rooms.Models;
using AccommodationBooking.Infrastructure.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Contexts
{
    public class AccommodationBookingContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AccommodationBookingContext(DbContextOptions<AccommodationBookingContext> options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entities = ChangeTracker.Entries()
            .Where(x =>
                (x.Entity is BaseEntity.Models.AuditEntity) &&
                (x.State == EntityState.Added || x.State == EntityState.Modified)
                );

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity.Models.AuditEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity.Models.AuditEntity)entity.Entity).UpdatedAt = now;
            }

            return await base.SaveChangesAsync(cancellationToken);
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
        }
    }
}