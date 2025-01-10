using AccommodationBooking.Domain.Hotels.Models;

namespace AccommodationBooking.Domain.Rooms.Models;

public class Room
{
    public int Id { get; set; }
    public string RoomNo { get; set; }
    public int? HotelId { get; set; }
    public int AdultsCapacity { get; set; } = 2;
    public int ChildrenCapacity { get; set; } = 0;
    public bool IsAvailable { get; set; } = true;
    public string? Description { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public RoomType RoomType { get; set; }
}