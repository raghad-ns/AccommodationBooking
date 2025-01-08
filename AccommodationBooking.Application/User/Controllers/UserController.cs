using AccommodationBooking.Application.Configuration.Authentication.Services;
using AccommodationBooking.Application.User.Mappers;
using AccommodationBooking.Application.User.Models;
using AccommodationBooking.Domain.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Application.User.Controllers;
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ApplicationDomainUserMapper _mapper;
    private readonly ITokensService _tokenService;

    public UserController(
        IUserService userService,
        ITokensService tokenService,
        ApplicationDomainUserMapper mapper
        )
    {
        _userService = userService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.User>>> GetUsers()
    {
        var users = await _userService.GetUsers();
        var applicationUsers = users.Select(user => _mapper.ToApplication(user)).ToList();
        return Ok(applicationUsers);
    }

    [HttpPost("register")]
    public async Task<ActionResult<Models.User>> Register([FromBody] Models.User userDTO)
    {
        var domainUser = _mapper.ToDomain(userDTO);
        var user = await _userService.Register(domainUser);
        
        if (user == null)
            return BadRequest();

        return Created();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginDTO)
    {
        var domainLogin = _mapper.ToDomain(loginDTO);
        var user = await _userService.Login(domainLogin);
        var applicationUser = _mapper.ToApplication(user);

        var token = _tokenService.GenerateToken(new Infrastructure.Users.Models.User
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            NormalizedUserName = user.FirstName + " " + user.LastName,

        });

        if (user == null)
            return Unauthorized();

        return Ok(new LoginResponse(applicationUser, token));
    }
}