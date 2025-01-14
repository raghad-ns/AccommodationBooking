using AccommodationBooking.Domain.Users.Exceptions;
using AccommodationBooking.Domain.Users.Repositories;
using AccommodationBooking.Infrastructure.Contexts;
using AccommodationBooking.Infrastructure.Users.Mappers;
using AccommodationBooking.Infrastructure.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace AccommodationBooking.Infrastructure.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AccommodationBookingContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signinManager;

    public UserRepository(
        AccommodationBookingContext context,
        UserManager<User> userManager,
        SignInManager<User> signinManager)
    {
        _context = context;
        _userManager = userManager;
        _signinManager = signinManager;
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

        return user.ToDomain(role);
    }

    //public Task Logout(string token)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<Domain.Users.Models.User> Register(Domain.Users.Models.User domainModel)
    {
        var model = domainModel.ToInfrastructure();
        model.NormalizedUserName = domainModel.FirstName + " " + domainModel.LastName;

        var similarUser = await _context.Users
            .FirstOrDefaultAsync(x =>
                x.Email == model.Email || x.UserName == model.UserName
            );

        if (similarUser != null)
            throw new UserAlreadyExistedException();

        return await _context.PerformTransaction<Domain.Users.Models.User>(async transaction =>
        {
            var createdUser = await _userManager.CreateAsync(model, domainModel.Password);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(model, "Admin");

                if (roleResult.Succeeded)
                {
                    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
                    var roles = await _userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault();

                    await transaction.CommitAsync();

                    return model.ToDomain(role);
                }

                await transaction.RollbackAsync();
                throw new InvalidOperationException("Failed to create user or assign role.");
            }
            await transaction.RollbackAsync();
            throw new InvalidOperationException("Failed to create user or assign role.");
        });
    }
}
