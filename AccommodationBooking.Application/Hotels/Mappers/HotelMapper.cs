﻿using AccommodationBooking.Application.Hotels.Models;
using Riok.Mapperly.Abstractions;
using DomainHotel = AccommodationBooking.Domain.Hotels.Models.Hotel;
using DomainHotelFilters = AccommodationBooking.Domain.Hotels.Models.HotelFilters;

namespace AccommodationBooking.Application.Hotels.Mappers;

[Mapper]
public partial class HotelMapper
{
    public partial DomainHotel ToDomain(Hotel hotel);
    public partial Hotel ToApplication(DomainHotel hotel);
    public partial DomainHotelFilters ToDomain(HotelFilters hotelFilters);
}