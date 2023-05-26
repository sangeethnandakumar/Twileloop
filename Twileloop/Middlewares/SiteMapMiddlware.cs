using Packages.Twileloop.Repository;
using Serilog;
using System.Text;
using System.Xml.Linq;

namespace Packages.Twileloop.Middlewares
{
    public class SitemapMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GitHubHandler gitHubHandler;

        public SitemapMiddleware(RequestDelegate next, GitHubHandler gitHubHandler)
        {
            _next = next;
            this.gitHubHandler = gitHubHandler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/sitemap.xml")
            {
                var baseUrl = $"https://packages.twileloop.com";

                // Generate the sitemap XML
                XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";

                var packages = await gitHubHandler.FetchPackagesAsync(APIConstants.REPOS_TO_DISCOVER.ToArray());
                packages = packages.OrderBy(x => x.Name).ToList();

                var packageTags = new List<XElement>
                {
                    new XElement(xmlns + "url",
                           new XElement(xmlns + "loc", $"{baseUrl}"),
                           new XElement(xmlns + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                           new XElement(xmlns + "changefreq", "weekly"),
                           new XElement(xmlns + "priority", "1.00")
                       )
                };

                foreach (var package in packages)
                {
                    packageTags.Add(new XElement(xmlns + "url",
                            new XElement(xmlns + "loc", $"{baseUrl}/{package.Name.ToLower()}"),
                            new XElement(xmlns + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                            new XElement(xmlns + "changefreq", "weekly"),
                            new XElement(xmlns + "priority", "1.00")
                        ));
                }
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