using System.Net;
using System.Text.Json;
using OrderManagementService.Core.Entities;

namespace OrderManagementService.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new RespuestaAPI
            {
                Mensaje = exception.Message,
                Exito = false,
                Datos = null
            };

            var logger = context.RequestServices.GetRequiredService<ILogger<GlobalExceptionMiddleware>>();

            switch (exception)
            {
                case ArgumentNullException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Mensaje = "Se proporcionó un argumento nulo";
                    logger.LogWarning($"BadRequest: {exception.Message}");
                    break;

                case ArgumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Mensaje = "Argumentos inválidos";
                    logger.LogWarning($"BadRequest: {exception.Message}");
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Mensaje = "No autorizado";
                    logger.LogWarning($"Unauthorized: {exception.Message}");
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Mensaje = "Error interno del servidor";
                    logger.LogError(exception, $"Unhandled exception: {exception.Message}");
                    break;
            }

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
