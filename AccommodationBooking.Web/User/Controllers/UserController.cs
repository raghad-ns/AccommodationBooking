using AccommodationBooking.Domain.Users.Services;
using AccommodationBooking.Web.Configuration.Authentication.Services;
using AccommodationBooking.Web.User.Mappers;
using AccommodationBooking.Web.User.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationBooking.Application.User.Controllers;
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokensService _tokenService;
    private const int CookieExpireInHours = 8;

    public UserController(
        IUserService userService,
        ITokensService tokenService
        )
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Web.User.Models.User>> Register([FromBody] Web.User.Models.User userDTO)
    {
        var domainUser = userDTO.ToDomain();
        var user = await _userService.Register(domainUser);

        if (user == null)
            return BadRequest();

        return Created();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginDTO)
    {
        var domainLogin = loginDTO.ToDomain();
        var user = await _userService.Login(domainLogin);
        var applicationUser = user.ToApplication();

        var token = _tokenService.GenerateToken(applicationUser);

        if (user == null)
            return Unauthorized();

        HttpContext.Response.Cookies.Append("Token", token, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddHours(CookieExpireInHours),
            HttpOnly = true, // Accessible only by the server
            IsEssential = true // Required for GDPR compliance
        });

        return Ok(new LoginResponse(applicationUser, token));
    }
}