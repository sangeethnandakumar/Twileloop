using HtmlAgilityPack;
using Markdig;
using Microsoft.Extensions.Caching.Memory;
using Octokit;
using Packages.Twileloop.Helpers;
using Packages.Twileloop.Models;
using System.Text;
using System.Text.Json;

namespace Packages.Twileloop.Repository
{
    public class GitHubHandler
    {
        private readonly IMemoryCache memoryCache;

        public GitHubHandler(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }


        public async Task<List<PackageInfo>> FetchPackagesAsync(string[] repoUrls)
        {   
            if (memoryCache.TryGetValue("PACKAGE_CATALOGUE", out List<PackageInfo> packageInfo))
            {
                return packageInfo.Shuffle().ToList(); ;
            }

            var packages = new List<PackageInfo>();
            packages = JsonSerializer.Deserialize<List<PackageInfo>>(System.IO.File.ReadAllText("data.json"));

            //var github = new GitHubClient(new ProductHeaderValue("Packages.Twileloop"));

            //foreach (var repoUrl in repoUrls)
            //{
            //    var repositoryUri = new Uri(repoUrl);
            //    var segments = repositoryUri.Segments;
            //    var owner = segments[segments.Length - 2].TrimEnd('/');
            //    var repo = segments[segments.Length - 1].TrimEnd('/');

            //    // Get the README.md content
            //    var readme = await github.Repository.Content.GetRawContent(owner, repo, "document.md");
            //    var markdownContent = Encoding.UTF8.GetString(readme);

            //    // Convert Markdown to HTML
            //    var pipeline = new MarkdownPipelineBuilder().Build();
            //    var htmlContent = Markdown.ToHtml(markdownContent, pipeline);
            //    var parsedHtml = ParseHtml(htmlContent);

            //    packages.Add(new PackageInfo
            //    {
            //        GithubURL = repoUrl,
            //        Name = repo,
            //        PackageIcon = parsedHtml.PackageIcon,
            //        Description = parsedHtml.Description,
            //        HTMLContent = htmlContent
            //    });
            //}

            //File.WriteAllText("data.json", JsonSerializer.Serialize(packages));

            memoryCache.Set("PACKAGE_CATALOGUE", packages, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });

            //File.WriteAllText("data.json", JsonSerializer.Serialize(packages));

            return packages.Shuffle().ToList();
        }

        private static (string PackageIcon, string Description) ParseHtml(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Extract the first image URL
            var imageUrlNode = doc.DocumentNode.SelectSingleNode("//img[@src]");
            string imageUrl = imageUrlNode?.GetAttributeValue("src", string.Empty);

            // Extract the description
            var descriptionNode = doc.DocumentNode.SelectSingleNode("//h2[text()='About']/following-sibling::p");
            string description = descriptionNode?.InnerHtml.Trim();

            return (imageUrl, description);
        }
    }
}
