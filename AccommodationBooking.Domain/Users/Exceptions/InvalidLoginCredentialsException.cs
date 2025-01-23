using AccommodationBooking.Domain.Exceptions.ClientError;

namespace AccommodationBooking.Domain.Users.Exceptions
{
    public class InvalidLoginCredentialsException() : BusinessException400("Incorrect username or password.");
}
