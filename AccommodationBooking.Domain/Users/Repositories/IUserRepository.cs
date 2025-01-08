﻿using AccommodationBooking.Domain.Users.Models;

namespace AccommodationBooking.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User> Login(LoginRequest loginDTO);
        //Task Logout(string token);
        Task<User> Register(User model);
        Task<List<User>> GetAllUsers(int page, int pageSize);
    }
}
