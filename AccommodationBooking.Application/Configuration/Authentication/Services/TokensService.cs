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
            Expires = DateTime.UtcNow.AddMinutes(ParseDuration(_authenticationOptions.ExpiresIn)),
            Issuer = _authenticationOptions.Issuer,
            Audience = _authenticationOptions.Audience,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static int ParseDuration(string duration)
    {
        if (string.IsNullOrWhiteSpace(duration))
            throw new ArgumentException("Duration string cannot be null or empty");

        var unit = duration[^1]; // Get the last character (e.g., 'h', 'm', 'd')
        var value = duration[..^1]; // Get everything except the last character

        if (!int.TryParse(value, out var numericValue))
            throw new ArgumentException($"Invalid duration value: {duration}");

        return unit switch
        {
            'm' => numericValue,  // Minutes
            'h' => numericValue * 60,    // Hours
            'd' => numericValue * 60 * 24,     // Days
            _ => throw new ArgumentException($"Invalid duration unit: {unit}")
        };
    }
}