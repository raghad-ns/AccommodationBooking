using Riok.Mapperly.Abstractions;
using DomainReview = AccommodationBooking.Domain.Reviews.Models.Review;
using DomainReviewFilters = AccommodationBooking.Domain.Reviews.Models.ReviewFilters;
using AccommodationBooking.Web.Reviews.Models;

namespace AccommodationBooking.Web.Reviews.Mappers;

[Mapper]
public static partial class ReviewMapper
{
    public static partial Review ToApplication(this DomainReview review);
    public static partial DomainReview ToDomain(this Review review);

    public static partial ReviewFilters ToApplication(this DomainReviewFilters filters);
    public static partial DomainReviewFilters ToDomain(this ReviewFilters filters);
}
