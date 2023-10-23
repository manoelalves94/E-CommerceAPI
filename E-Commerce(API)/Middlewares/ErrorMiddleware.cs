using E_Commerce_API_.Data.DTOs.Error;
using E_Commerce_API_.Exceptions;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace E_Commerce_API_.Middlewares;

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
        catch (Exception ex)
		{
			await HandleException(context, ex);
		}
	}

    private Task HandleNotFoundException(HttpContext context, NotFoundException ex)
    {
        var errorResponseDto = new ErrorResponseDto(HttpStatusCode.NotFound.ToString(), ex.Message);

        Log.Error("Erro: {@errorResponseDto}", errorResponseDto);

        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

        return JsonResponse(context, errorResponseDto);
    }

    private Task HandleBadRequestException(HttpContext context, BadRequestException ex)
    {
        var errorResponseDto = new ErrorResponseDto(HttpStatusCode.BadRequest.ToString(), ex.Message);

        Log.Error("Erro: {@errorResponseDto}", errorResponseDto);

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return JsonResponse(context, errorResponseDto);
    }

    private Task HandleException(HttpContext context, Exception ex)
    {
		var errorResponseDto = new ErrorResponseDto(HttpStatusCode.InternalServerError.ToString(), ex.Message);

		Log.Error("Ocorreu uma exceção: {@errorResponseDto}", errorResponseDto);
		
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
		{
            errorResponseDto.GenericError();
		}

        return JsonResponse(context, errorResponseDto);
    }

    private static Task JsonResponse(HttpContext context, ErrorResponseDto errorResponseDto)
    {
        var result = JsonConvert.SerializeObject(errorResponseDto);
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(result);
    }
}
