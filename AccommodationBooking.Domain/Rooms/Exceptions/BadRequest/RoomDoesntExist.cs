using AccommodationBooking.Domain.Exceptions.ClientError;

namespace AccommodationBooking.Domain.Rooms.Exceptions.BadRequest;

public class RoomDoesntExist() : NotFound404("Room doesn't exist!");