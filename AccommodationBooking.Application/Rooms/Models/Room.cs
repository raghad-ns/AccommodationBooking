using AccommodationBooking.Application.Hotels.Models;
using AccommodationBooking.Domain.Rooms.Models;

namespace AccommodationBooking.Application.Rooms.Models;

public class Room
{
    public int Id { get; set; }
    public string RoomNo { get; set; }
    public Hotel Hotel { get; set; }
    public int AdultsCapacity { get; set; } = 2;
    public int ChildrenCapacity { get; set; } = 0;
    public bool IsAvailable { get; set; } = true;
    public string Description { get; set; }
    public List<string> Images { get; set; }
    public RoomType RoomType { get; set; }
}
