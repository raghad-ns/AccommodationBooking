using AccommodationBooking.Domain.Users.Exceptions;

namespace AccommodationBooking.Application.Middlewares.ExceptionsHandler;

public class ExceptionsHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionsHandler> _logger;

    public ExceptionsHandler(RequestDelegate next, ILogger<ExceptionsHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext); // calling next middleware
        }
        catch (InvalidLoginCredentialsException ex)
        {
            var response = httpContext.Response;
            response.StatusCode = 400;
            await response.WriteAsync(ex.Message);
        }
        catch(UserAlreadyExistedException ex)
        {
            var response = httpContext.Response;
            response.StatusCode = 400;
            await response.WriteAsync(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            var response = httpContext.Response;
            response.StatusCode = 404;
            await response.WriteAsync(ex.Message);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString(), ex);

            var response = httpContext.Response;
            response.StatusCode = 500;
            await response.WriteAsync("Something went wrong, please try again later");
        }
    }
}