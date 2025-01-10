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
    public async Task<ActionResult<IEnumerable<Models.User>>> GetUsers([FromQuery] int page, [FromQuery] int pageSize)
    {
        var users = await _userService.GetUsers(page, pageSize);
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

        var token = _tokenService.GenerateToken(applicationUser);

        if (user == null)
            return Unauthorized();

        HttpContext.Response.Cookies.Append("Token", token, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddHours(8),
            HttpOnly = true, // Accessible only by the server
            IsEssential = true // Required for GDPR compliance
        });

        return Ok(new LoginResponse(applicationUser, token));
    }
}