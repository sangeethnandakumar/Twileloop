using Serilog;

namespace Twileloop.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool shouldRedirect = false;
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                shouldRedirect = true;
            }
            if (shouldRedirect)
            {
                context.Response.Redirect($"/error");
                return;
            }
        }
    }

}
