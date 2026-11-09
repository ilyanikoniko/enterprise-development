using System.Net;
using System.Text.Json;
using DryCleaner.Application.Exceptions;

namespace DryCleaner.Api.Middleware;

/// <summary>
/// Глобальный обработчик исключений, превращает исключения в HTTP-коды
/// </summary>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BadRequestException ex) 
        {
            logger.LogWarning("Ошибка запроса: {message}", ex.Message);
            await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (ArgumentException ex) 
        {
            logger.LogWarning("Некорректные данные: {message}", ex.Message);
            await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "Некорректные данные в запросе.");
        }
        catch (KeyNotFoundException ex) 
        {
            logger.LogWarning("Не найдено: {message}", ex.Message);
            await HandleExceptionAsync(context, HttpStatusCode.NotFound, "Запрашиваемый ресурс не найден.");
        }
        catch (Exception ex) 
        {
            logger.LogError(ex, "Внутренняя ошибка сервера");
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Внутренняя ошибка сервера. " +
                "Пожалуйста, повторите попытку позже.");
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        var response = new { error = message };

        var options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }
}