using AccommodationBooking.Infrastructure.Reviews.Models;
using Riok.Mapperly.Abstractions;
using DomainReview = AccommodationBooking.Domain.Reviews.Models.Review;

namespace AccommodationBooking.Infrastructure.Reviews.Mappers;

[Mapper]
public static partial class ReviewsMapper
{
    public static partial Review ToInfrastructure(this DomainReview review);
    public static partial DomainReview ToDomain(this Review review);
    public static partial void ToInfrastructureUpdate(this DomainReview source, Review target);
}
