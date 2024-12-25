using AccommodationBooking.Domain.User.Models;
using AccommodationBooking.Domain.User.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.User.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AccommodationBookingContext _context;

    public UserRepository(AccommodationBookingContext context)
    {
        _context = context;
    }
    public Task<List<UserModel>> GetAllUsers()
    {
        //var users = await _context.Users.ToListAsync();
        //Console.WriteLine("users: ", users);
        return _context.Users.ToListAsync();
    }

    public Task<UserModel> Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task Logout(string token)
    {
        throw new NotImplementedException();
    }

    public async Task<UserModel> Register(UserModel model)
    {
        var users = await _context.Users.ToListAsync();
        Console.WriteLine("users", users);
        var similarUser = await _context.Users
            .FirstOrDefaultAsync(x =>
                (x.Email == model.Email || x.UserName == model.UserName)
            );
        if (similarUser != null)
        {
            Console.WriteLine("similar: ", similarUser);
            return null;
        }

        _context.Users.AddAsync(model);
        _context.SaveChanges();
        var user =  await _context.Users.FirstOrDefaultAsync(user => user.Email == model.Email);
        Console.WriteLine(user);
        return user;
    }
}
