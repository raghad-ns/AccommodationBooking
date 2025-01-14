﻿using AccommodationBooking.Application.User.Models;
using Riok.Mapperly.Abstractions;

namespace AccommodationBooking.Application.User.Mappers;

[Mapper]
public static partial class ApplicationDomainUserMapper
{
    public static partial Domain.Users.Models.LoginRequest ToDomain(this LoginRequest loginRequest);
    public static partial LoginRequest ToApplication(this Domain.Users.Models.LoginRequest loginRequest);
    public static partial Domain.Users.Models.User ToDomain(this Models.User user);
    public static partial Models.User ToApplication(this Domain.Users.Models.User user);
}