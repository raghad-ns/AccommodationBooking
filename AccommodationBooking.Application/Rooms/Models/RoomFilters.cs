namespace AccommodationBooking.Application.Rooms.Models;

public class RoomFilters
{
    public int? Id { get; set; }
    public string? RoomNo { get; set; }
    public string? HotelName { get; set; }
    public double? AdultsCapacityFrom { get; set; }
    public double? AdultsCapacityTo { get; set; }
    public double? ChildrenCapacityFrom { get; set; }
    public double? ChildrenCapacityTo { get; set; }
    public bool? IsAvailable { get; set; } = true;
    public string? Description { get; set; }
    public List<string>? RoomTypes { get; set; }
}