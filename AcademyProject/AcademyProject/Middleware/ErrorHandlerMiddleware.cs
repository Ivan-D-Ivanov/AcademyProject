using System.Net;
using Newtonsoft.Json;

namespace AcademyProject.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                switch (error)
                {
                    case ApplicationException e: response.StatusCode = (int)HttpStatusCode.BadRequest; break;
                    case KeyNotFoundException e: response.StatusCode = (int)HttpStatusCode.NotFound; break;
                    default: response.StatusCode = (int)HttpStatusCode.InternalServerError; break;
                }

                var result = JsonConvert.SerializeObject(new {message = error.Message});
                _logger.Log(LogLevel.Error, result);
                await response.WriteAsync(result);
            }
        }
    }
}
