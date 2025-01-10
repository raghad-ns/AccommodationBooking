namespace AccommodationBooking.Application.Hotels.Models;

public class HotelSummary
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; }
    public int? CityId { get; set; }
    public double StarRating { get; set; }
}