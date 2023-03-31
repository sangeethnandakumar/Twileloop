using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Xml.Linq;

namespace Twileloop.Middlewares {
    public class SitemapMiddleware {
        private readonly RequestDelegate _next;

        public SitemapMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IRazorViewEngine razorViewEngine) {
            if (context.Request.Path == "/sitemap.xml") {
                // Get all the Razor pages in the application
                var razorPages = actionDescriptorCollectionProvider.ActionDescriptors.Items
                    .OfType<PageActionDescriptor>()
                    .Where(ad => ad.RouteValues.ContainsKey("page"));

                // Get the base URL of the application
                var request = context.Request;
                var scheme = request.Scheme;
                var host = request.Host;
                var pathBase = request.PathBase;
                var baseUrl = $"{scheme}://{host}{pathBase}";

                // Generate the sitemap XML
                XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                var sitemap = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes"),
                    new XElement(xmlns + "urlset",
                        new XElement(xmlns + "url",
                            new XElement(xmlns + "loc", $"{baseUrl}/"),
                            new XElement(xmlns + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                            new XElement(xmlns + "priority", "1.00")
                        ),
                        new XElement(xmlns + "url",
                            new XElement(xmlns + "loc", $"{baseUrl}/features"),
                            new XElement(xmlns + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                            new XElement(xmlns + "priority", "0.80")
                        ),
                        new XElement(xmlns + "url",
                            new XElement(xmlns + "loc", $"{baseUrl}/pricing"),
                            new XElement(xmlns + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                            new XElement(xmlns + "priority", "0.80")
                        ),
                        new XElement(xmlns + "url",
                            new XElement(xmlns + "loc", $"{baseUrl}/about"),
                            new XElement(xmlns + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                            new XElement(xmlns + "priority", "0.80")
                        )
                    )
                );

                // Return the sitemap XML as the response
                context.Response.ContentType = "application/xml";
                await context.Response.WriteAsync(sitemap.ToString(), Encoding.UTF8);
            }
            else {
                // Pass the request to the next middleware in the pipeline
                await _next(context);
            }
        }
    }

    public static class SitemapMiddlewareExtensions {
        public static IApplicationBuilder UseSitemapMiddleware(this IApplicationBuilder app) {
            return app.UseMiddleware<SitemapMiddleware>();
        }
    }
}