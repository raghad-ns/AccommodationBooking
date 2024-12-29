using AccommodationBooking.Application.User.Mappers;
using AccommodationBooking.Application.User.Models;
using AccommodationBooking.Domain.User.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Application.User.Controllers;
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ApplicationDomainUserMapper _mapper;

    public UserController(
        IUserService userService,
        ApplicationDomainUserMapper mapper
        )
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Domain.Users.Models.User>>> GetUsers()
    {
        var users = await _userService.GetUsers();
        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<ActionResult<Domain.Users.Models.User>> Register([FromBody] Domain.Users.Models.User userDTO)
    {
        var user = await _userService.Register(userDTO);
        if (user == null)
            return BadRequest();

        return Created();
    }

    [HttpPost("login")]
    public async Task<ActionResult<Domain.Users.Models.User>> Login([FromBody] LoginRequest loginDTO)
    {
        var domainLogin = _mapper.ToDomainLoginRequest(loginDTO);
        var user = await _userService.Login(domainLogin);

        if (user == null)
            return Unauthorized();

        return Ok(user);
    }
}