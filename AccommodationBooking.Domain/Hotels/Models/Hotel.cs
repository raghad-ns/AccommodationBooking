﻿using AccommodationBooking.Domain.Rooms.Models;

namespace AccommodationBooking.Domain.Hotels.Models;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; }
    public int? CityId { get; set; }
    public double StarRating { get; set; }
    public IReadOnlyCollection<string> Images { get; set; } = new List<string>();
    public IReadOnlyCollection<Amenity> Amenities { get; set; } = new List<Amenity>();
    public IReadOnlyCollection<Room> Rooms { get; set; } = new List<Room>();
}
