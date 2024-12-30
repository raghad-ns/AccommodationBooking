
using AccommodationBooking.Application.Configuration.Authentication.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccommodationBooking.Application.Configuration.Authentication.Services;

public class TokensService : ITokensService
{
    private readonly AuthenticationOptions _authenticationOptions;
    private readonly SymmetricSecurityKey _symmetricSecurityKey;

    public TokensService(IOptions<AuthenticationOptions> authenticationOptions)
    {
        _authenticationOptions = authenticationOptions.Value;
        var secretKey = _authenticationOptions.Key;
        _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    }

    string ITokensService.GenerateToken(Infrastructure.Users.Models.User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName ?? ""),
        };

        var signinCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = signinCredentials,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_authenticationOptions.ExpiresIn),
            Issuer = _authenticationOptions.Issuer,
            Audience = _authenticationOptions.Audience,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}