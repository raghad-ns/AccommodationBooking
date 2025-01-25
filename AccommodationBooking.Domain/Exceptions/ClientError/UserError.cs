namespace AccommodationBooking.Domain.Exceptions.ClientError;

public class UserError(string message) : Exception(message);