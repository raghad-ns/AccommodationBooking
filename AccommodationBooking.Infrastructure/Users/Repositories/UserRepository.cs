//using AccommodationBooking.Domain.User.Models;
using AccommodationBooking.Domain.User.Repositories;
using AccommodationBooking.Domain.Users.Models;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Users.Mappers;
using AccommodationBooking.Infrastructure.Users.Models;

//using AccommodationBooking.Infrastructure.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AccommodationBookingContext _context;
    private readonly UserMapper _mapper;

    public UserRepository(AccommodationBookingContext context, UserMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<List<Domain.Users.Models.User>> GetAllUsers()
    {
        //var users = await _context.Users.ToListAsync();
        return _context.Users
            .Select(user => _mapper.ToDomainUser(user))
            .ToListAsync();
    }

    public async Task<Domain.Users.Models.User> Login(LoginRequest loginDTO)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.UserName);
        if (user == null)
        {
            return null;
        }
        if (user.Password != loginDTO.Password)
        {
            return null;
        }
        return _mapper.ToDomainUser(user);
    }

    //public Task Logout(string token)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<Domain.Users.Models.User> Register(Domain.Users.Models.User domainModel)
    {
        var model = _mapper.ToInfrastructureUser(domainModel);
        var users = await _context.Users.ToListAsync();
        var similarUser = await _context.Users
            .FirstOrDefaultAsync(x =>
                x.Email == model.Email || x.UserName == model.UserName
            );

        if (similarUser != null)
        {
            Console.WriteLine("similar: ", similarUser);
            return null;
        }

        _context.Users.Add(model);
        await _context.SaveChangesAsync();
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == model.Email);

        return _mapper.ToDomainUser(user);
    }
}
