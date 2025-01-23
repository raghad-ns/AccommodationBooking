using AccommodationBooking.Domain.Exceptions.ClientError;
using AccommodationBooking.Domain.Users.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AccommodationBooking.Web.Middlewares.ExceptionsHandler;

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
        catch (Exception e)
        when (e is BusinessException400)
        {
            _logger.LogError(e.ToString(), e);

            var response = httpContext.Response;
            response.StatusCode = 400;
            await response.WriteAsync(e.Message);
        }
        catch (Exception e)
        when (e is NotFound404)
        {
            _logger.LogError(e.ToString(), e);

            var response = httpContext.Response;
            response.StatusCode = 404;
            await response.WriteAsync(e.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString(), ex);

            var response = httpContext.Response;
            response.StatusCode = 503;
            await response.WriteAsync("Something went wrong, please try again later");
        }
    }
}