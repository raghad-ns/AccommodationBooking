using AccommodationBooking.Domain.Users.Exceptions;
using AccommodationBooking.Domain.Users.Repositories;
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
    public async Task<List<Domain.Users.Models.User>> GetAllUsers(int page, int pageSize)
    {
        var users = await _userManager.Users
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(user => new
            {
                User = user,
                Roles = _userManager.GetRolesAsync(user).Result
            })
            .ToListAsync(); 

        var domainUsers = users
            .Select(user => _mapper.ToDomain(user.User, user.Roles.FirstOrDefault()))
            .ToList();

        return domainUsers.ToList(); 
    }

    public async Task<Domain.Users.Models.User> Login(Domain.Users.Models.LoginRequest loginDTO)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.UserName);
        if (user == null)
            throw new InvalidLoginCredentialsException();

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault();

        var result = await _signinManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
        if (result == null) throw new InvalidLoginCredentialsException(); ;

        return _mapper.ToDomain(user, role);
    }

    //public Task Logout(string token)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<Domain.Users.Models.User> Register(Domain.Users.Models.User domainModel)
    {
        var model = _mapper.ToInfrastructure(domainModel);
        model.NormalizedUserName = domainModel.FirstName + " " + domainModel.LastName;
        model.Id = Guid.NewGuid().ToString();

        var similarUser = await _context.Users
            .FirstOrDefaultAsync(x =>
                x.Email == model.Email || x.UserName == model.UserName
            );

        if (similarUser != null)
            throw new UserAlreadyExistedException();

        var createdUser = await _userManager.CreateAsync(model, domainModel.Password);
        if (createdUser.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(model, "User");

            if (roleResult.Succeeded)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                return _mapper.ToDomain(model, role);
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
