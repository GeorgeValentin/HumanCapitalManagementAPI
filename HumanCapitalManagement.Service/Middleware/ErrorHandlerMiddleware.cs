using FluentValidation;
using HumanCapitalManagement.Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Text.Json;

namespace HumanCapitalManagement.Service.Middleware;
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        } catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            string result = string.Empty;

            switch (error)
            {
                case ApplicationException:
                case InvalidDateException:
                case NotSupportedException:
                case ValidationException:
                case UnsetCacheException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { message = error?.Message });
                    
                    Log.Error("The following error has happened {error}.", error?.Message);

                    break;

                case KeyNotFoundException:
                case ArgumentException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new { message = error?.Message });

                    Log.Error("The following error has happened {error}.", error?.Message);

                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize("Something went wrong, we are sorry!");

                    Log.Error("The following error has happened {error}.", error?.Message);

                    break;
            }

            await response.WriteAsync(result);
        }
    }
}
