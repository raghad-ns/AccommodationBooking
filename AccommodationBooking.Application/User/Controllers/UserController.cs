using AccommodationBooking.Application.User.Models;
using AccommodationBooking.Domain.User.Models;
using AccommodationBooking.Domain.User.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Application.User.Controllers;
[ApiController]
[Route("/user")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService; 
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }

    [HttpPost("/register")]
    public async Task<ActionResult<UserModel>> Register(UserModel userDTO)
    {
        var user = _userService.Register(userDTO);
        return Created();
    }
}