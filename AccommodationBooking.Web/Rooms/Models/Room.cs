using AccommodationBooking.Domain.Rooms.Models;

namespace AccommodationBooking.Web.Rooms.Models;

public class Room
{
    public int Id { get; init; }
    public string RoomNo { get; init; }
    public int? HotelId { get; init; }
    public int AdultsCapacity { get; init; } = 2;
    public int ChildrenCapacity { get; init; } = 0;
    public bool IsAvailable { get; init; } = true;
    public string? Description { get; init; }
    public IReadOnlyCollection<string> Images { get; init; } = new List<string>();
    public RoomType RoomType { get; init; }
}