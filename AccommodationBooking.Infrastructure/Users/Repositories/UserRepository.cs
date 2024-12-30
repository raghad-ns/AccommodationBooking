using AccommodationBooking.Domain.User.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Users.Mappers;
using AccommodationBooking.Infrastructure.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBooking.Infrastructure.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AccommodationBookingContext _context;
    private readonly UserMapper _mapper;
    private readonly UserManager<User> _userManager;

    public UserRepository(AccommodationBookingContext context, UserMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }
    public Task<List<Domain.Users.Models.User>> GetAllUsers()
    {
        //var users = await _context.Users.ToListAsync();
        return _context.Users
            .Select(user => _mapper.ToDomainUser(user))
            .ToListAsync();
    }

    public async Task<Domain.Users.Models.User> Login(Domain.Users.Models.LoginRequest loginDTO)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.UserName);
        if (user == null)
        {
            return null;
        }
        if (user.PasswordHash != loginDTO.Password)
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
        model.Id = Guid.NewGuid().ToString();

        var similarUser = await _context.Users
            .FirstOrDefaultAsync(x =>
                x.Email == model.Email || x.UserName == model.UserName
            );

        if (similarUser != null)
        {
            return null;
        }

        var createdUser = await _userManager.CreateAsync(model, domainModel.Password);
        if (createdUser.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(model, "User");

            if (roleResult.Succeeded)
            {
                return _mapper.ToDomainUser(model);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
