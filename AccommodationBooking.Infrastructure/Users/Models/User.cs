using AccommodationBooking.Domain.Users.Models;
using AccommodationBooking.Infrastructure.Reviews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Users.Models;

public class User: IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Review> Reviews { get; set; } = new List<Review>();

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(user => user.Reviews)
                .WithOne(review => review.User)
                .HasForeignKey(review => review.UserId)
                .HasPrincipalKey(user => user.Id);
        }
    }
}