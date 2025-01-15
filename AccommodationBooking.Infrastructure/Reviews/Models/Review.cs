﻿using AccommodationBooking.Infrastructure.Hotels.Models;
using AccommodationBooking.Infrastructure.Rooms.Models;
using AccommodationBooking.Infrastructure.Users.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccommodationBooking.Infrastructure.Reviews.Models;

public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Hotel Hotel { get; set; }
    public int HotelId { get; set; }
    public Room Room { get; set; }
    public int RoomId { get; set; }
    public string? Comment { get; set; } = string.Empty;
    public double? StarRating { get; set; }
}