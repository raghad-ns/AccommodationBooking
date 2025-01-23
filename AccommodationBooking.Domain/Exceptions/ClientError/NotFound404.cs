namespace AccommodationBooking.Domain.Exceptions.ClientError;

public class NotFound404(string message) : Exception(message);