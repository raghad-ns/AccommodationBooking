namespace AccommodationBooking.Domain.Exceptions.ClientError;

public class BusinessException400(string message) : Exception(message);