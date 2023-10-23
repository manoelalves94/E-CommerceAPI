using System.ComponentModel.DataAnnotations;
using System.Net;
using Newtonsoft.Json;
using Usuarios_API_.Data.Responses;
using Usuarios_API_.Exceptions;

namespace Usuarios_API_.Middlewares;

public class ErrorMiddleware
{
    private readonly RequestDelegate next;

    public ErrorMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleNotFoundException(context, ex);
        }
        catch (BadRequestException ex)
        {
            await HandleBadRequestException(context, ex);
        }
        catch (AggregateException ex)
        {
            await HandleAggregateExceptions(context, ex);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private Task HandleNotFoundException(HttpContext context, NotFoundException ex)
    {
        var errorMessage = new List<string>() { ex.Message };
        var errorResponseDto = new ErrorResponse(HttpStatusCode.NotFound.ToString(), errorMessage);

        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

        return JsonResponse(context, errorResponseDto);
    }

    private Task HandleBadRequestException(HttpContext context, BadRequestException ex)
    {
        var errorMessage = new List<string>() { ex.Message };
        var errorResponseDto = new ErrorResponse(HttpStatusCode.BadRequest.ToString(), errorMessage);

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return JsonResponse(context, errorResponseDto);
    }

    private Task HandleAggregateExceptions(HttpContext context, AggregateException ex)
    {
        var errorMessages = new List<string>();
        foreach (var exception in ex.InnerExceptions) { errorMessages.Add(exception.Message); }

        var errorResponseDto = new ErrorResponse(HttpStatusCode.BadRequest.ToString(), errorMessages);

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return JsonResponse(context, errorResponseDto);
    }

    private Task HandleException(HttpContext context, Exception ex)
    {
        var errorMessage = new List<string>() { ex.Message };
        var errorResponseDto = new ErrorResponse(HttpStatusCode.InternalServerError.ToString(), errorMessage);

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
        {
            errorResponseDto.Errors.InternalError();
        }

        return JsonResponse(context, errorResponseDto);
    }

    private static Task JsonResponse(HttpContext context, ErrorResponse errorResponseDto)
    {
        var result = JsonConvert.SerializeObject(errorResponseDto);
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(result);
    }
}
