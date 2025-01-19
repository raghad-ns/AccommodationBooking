namespace AccommodationBooking.Application.Rooms.Models;

public class RoomFilters
{
    public int? Id { get; init; }
    public string? RoomNo { get; init; }
    public string? HotelName { get; init; }
    public double? AdultsCapacityFrom { get; init; }
    public double? AdultsCapacityTo { get; init; }
    public double? ChildrenCapacityFrom { get; init; }
    public double? ChildrenCapacityTo { get; init; }
    public bool? IsAvailable { get; init; } = true;
    public string? Description { get; init; }
    public IReadOnlyCollection<string>? RoomTypes { get; init; }
}