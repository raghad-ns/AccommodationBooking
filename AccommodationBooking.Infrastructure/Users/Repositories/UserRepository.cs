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
    private readonly SignInManager<User> _signinManager;

    public UserRepository(
        AccommodationBookingContext context,
        UserMapper mapper,
        UserManager<User> userManager,
        SignInManager<User> signinManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _signinManager = signinManager;
    }
    public async Task<List<Domain.Users.Models.User>> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync(); 

        var domainUsers = await Task.WhenAll(
            users.Select(async user =>
            {
                var role = await _mapper.ToDomainRole(user);
                return _mapper.ToDomainUser(user, role);
            })
        );

        return domainUsers.ToList(); 
    }

    public async Task<Domain.Users.Models.User> Login(Domain.Users.Models.LoginRequest loginDTO)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.UserName);
        if (user == null)
        {
            return null;
        }

        var result = _signinManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
        if (!result.IsCompletedSuccessfully)
        {
            return null;
        }

        var role = await _mapper.ToDomainRole(user);
        return _mapper.ToDomainUser(user, role);
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
                var role = await _mapper.ToDomainRole(
                    await _userManager.Users.FirstOrDefaultAsync(u => u.Id == model.Id)
                    );
                return _mapper.ToDomainUser(model, role);
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
