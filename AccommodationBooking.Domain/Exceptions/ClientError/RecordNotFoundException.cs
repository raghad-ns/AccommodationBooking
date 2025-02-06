namespace AccommodationBooking.Domain.Exceptions.ClientError;

public class RecordNotFoundException<T>(string type, string property, T id)
    : Exception($"{type} not found: \n {property}: {id}");