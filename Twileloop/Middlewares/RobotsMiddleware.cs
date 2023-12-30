public class RobotsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly List<string> disallowedPaths;

    public RobotsMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        disallowedPaths = configuration.GetSection("RobotsExclusion").Get<List<string>>() ?? new List<string>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/robots.txt"))
        {
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync($"User-agent: *\nDisallow: {string.Join("\nDisallow: ", disallowedPaths)}");
        }
        else
        {
            await _next(context);
        }
    }
}
