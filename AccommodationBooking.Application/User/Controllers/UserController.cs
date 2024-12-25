using AccommodationBooking.Application.User.Models;
using AccommodationBooking.Domain.User.Models;
using AccommodationBooking.Domain.User.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Application.User.Controllers;
[ApiController]
[Route("api/users")]
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
    public async Task<ActionResult<UserModel>> Register([FromBody] UserModel userDTO)
    {
        var user = _userService.Register(userDTO);
        if (user == null)
            return BadRequest();

        return Created();
    }

    [HttpPost("/login")]
    public async Task<ActionResult<UserModel>> Login([FromBody] LoginDTO loginDTO)
    {
        var user = await _userService.Login(loginDTO);

        if (user == null)
            return Unauthorized();

        return Ok(user);
    }
}