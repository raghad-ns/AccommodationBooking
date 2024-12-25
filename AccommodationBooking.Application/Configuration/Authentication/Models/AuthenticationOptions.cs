namespace AccommodationBooking.Application.Configuration.Authentication.Models;

public class AuthenticationOptions
{
    public const string Authentication = "Authentication";
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiresIn { get; set; }
}
