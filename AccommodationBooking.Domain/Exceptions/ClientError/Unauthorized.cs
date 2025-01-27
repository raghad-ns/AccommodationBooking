namespace AccommodationBooking.Domain.Exceptions.ClientError;

public class Unauthorized(string message) : Exception(message);