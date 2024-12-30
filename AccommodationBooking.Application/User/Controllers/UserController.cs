using AccommodationBooking.Application.Configuration.Authentication.Services;
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
        return Ok(new
        {
            Id = user.Id,
            UserName= user.UserName, 
            Email =user.Email, 
            NormalizedUserName = user.FirstName + " " + user.LastName,
            Token= _tokenService.GenerateToken(new Infrastructure.Users.Models.User
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                NormalizedUserName = user.FirstName + " " + user.LastName,
                
            })
        });
    }
}