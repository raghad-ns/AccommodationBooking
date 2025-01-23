using AccommodationBooking.Domain.Exceptions.ClientError;

namespace AccommodationBooking.Domain.Users.Exceptions;

public class UserAlreadyExistedException() : BusinessException400("Username or email already existed");