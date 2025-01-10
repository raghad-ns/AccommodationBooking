using AccommodationBooking.Domain.Users.Exceptions;
using System;

namespace AccommodationBooking.Application.Middlewares.ExceptionsHandler;

public class ExceptionsHandler
{
    private readonly RequestDelegate _next;
    public ExceptionsHandler(RequestDelegate next)
    {
        _next = next;
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
            Console.WriteLine(ex.Message);
            var response = httpContext.Response;
            response.StatusCode = 500;
            await response.WriteAsync("Something went wrong, please try again later");
        }
    }
}