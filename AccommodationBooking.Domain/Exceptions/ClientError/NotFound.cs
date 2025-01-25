namespace AccommodationBooking.Domain.Exceptions.ClientError;

public class NotFound(string message) : Exception(message);