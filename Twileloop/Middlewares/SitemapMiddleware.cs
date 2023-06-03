using System;
using System.Text;
using System.Xml.Linq;

namespace Twileloop.Middlewares
{
    public class SitemapMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public SitemapMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/sitemap.xml")
            {
                var baseUrl = $"https://twileloop.com";

                // Generate the sitemap XML
                XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";

                var packageTags = new List<XElement>();

                var viewsPath = Path.Combine(_environment.ContentRootPath, "Views");
                var cshtmlFiles = Directory.GetFiles(viewsPath, "*.cshtml", SearchOption.AllDirectories);

                packageTags.Add(new XElement(xmlns + "url",
                            new XElement(xmlns + "loc", "https://twileloop.com"),
                            new XElement(xmlns + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                            new XElement(xmlns + "changefreq", "weekly"),
                            new XElement(xmlns + "priority", "1.00")
                        ));
                packageTags.Add(new XElement(xmlns + "url",
                           new XElement(xmlns + "loc", "https://twileloop.com/portfolio"),
                           new XElement(xmlns + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                           new XElement(xmlns + "changefreq", "weekly"),
                           new XElement(xmlns + "priority", "1.00")
                       ));

                var sitemap = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), new XElement(xmlns + "urlset", packageTags));

                // Return the sitemap XML as the response
                context.Response.ContentType = "application/xml";
                await context.Response.WriteAsync(sitemap.ToString(), Encoding.UTF8);
            }
            else
            {
                // Pass the request to the next middleware in the pipeline
                await _next(context);
            }
        }
    }

    public static class SitemapMiddlewareExtensions
    {
        public static IApplicationBuilder UseSitemapMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SitemapMiddleware>();
        }
    }

}
