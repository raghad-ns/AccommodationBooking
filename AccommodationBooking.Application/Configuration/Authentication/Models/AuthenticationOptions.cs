namespace AccommodationBooking.Application.Configuration.Authentication.Models;

public class AuthenticationOptions
{
    public const string Authentication = "Authentication";
    public string Key { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string ExpiresIn { get; init; }
}