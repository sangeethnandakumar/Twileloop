using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Packages.Twileloop.Repository;
using Packages.Twileloop.ViewModels;
using Serilog;

namespace Packages.Twileloop.Controllers
{
    public class HomeController : Controller
    {
        private readonly GitHubHandler githubHandler;

        public HomeController(GitHubHandler githubHandler)
        {
            this.githubHandler = githubHandler;
        }

        [HttpGet]
        [Route("{packageId}")]
        public async Task<IActionResult> Packages([FromRoute] string packageId)
        {
            if (packageId.Contains("robots.txt"))
            {
                Log.Information("Robot exclusion skipped");
                return NotFound();
            }
            var packageInfo = githubHandler.FetchPackagesAsync(APIConstants.REPOS_TO_DISCOVER.ToArray()).Result;
            var packageVm = new PackageViewModel
            {
                RecommendedPackages = packageInfo,
                ActivePackage = packageInfo.FirstOrDefault(x => x.Name.ToLower() == packageId.ToLower())
            };
            if(packageVm.ActivePackage is null)
            {
                return Redirect($"/");
            }
            packageVm.ActivePackage.HTMLContent = ProcessHTML(packageVm.ActivePackage.HTMLContent);
            Log.Information("Visited {@Page}", packageId);
            return View(packageVm);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var packageInfo = githubHandler.FetchPackagesAsync(APIConstants.REPOS_TO_DISCOVER.ToArray()).Result;
            //Make ViewModel
            var indexVM = new IndexViewModel
            {
                RecommendedPackages = packageInfo
            };
            Log.Information("Visited {@Page}", "Index");
            return View(indexVM);
        }

        public string ProcessHTML(string html)
        {
            // Load the HTML document
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            // Find all <p> tags
            HtmlNodeCollection paragraphNodes = document.DocumentNode.SelectNodes("//p");

            if (paragraphNodes != null)
            {
                foreach (HtmlNode paragraphNode in paragraphNodes)
                {
                    string paragraphText = paragraphNode.InnerHtml.Trim();

                    // Check if the paragraph starts with "| "
                    if (paragraphText.StartsWith("| "))
                    {
                        // Call FunctionA to process the content and convert it into a table
                        string processedTable = GenerateTableFromMarkDown(paragraphText);

                        // Create a new table node
                        HtmlNode tableNode = HtmlNode.CreateNode(processedTable);

                        // Replace the <p> node with the new table node
                        paragraphNode.ParentNode.ReplaceChild(tableNode, paragraphNode);
                    }
                }
            }

            // Return the modified HTML
            return document.DocumentNode.OuterHtml;
        }

        private string GenerateTableFromMarkDown(string tableMarkdown)
        {
            var output = "<table class=\"table table-dark\">";
            var rows = tableMarkdown.Split("\n");
            var maxColumns = 0; // Track the maximum number of columns
            var rowList = new List<List<string>>(); // Store rows as a list of lists

            for (var i = 0; i < rows.Length; i++)
            {
                if (i == 0)
                {
                    // Heading
                    var cols = rows[i].Split("| ").Where(x => !string.IsNullOrEmpty(x));
                    maxColumns = cols.Count(); // Update the maximum column count
                    output += "<thead class=\"thead-dark\"><tr>";

                    foreach (var col in cols)
                    {
                        output += $"<th scope=\"col\">{col.Trim()}</th>";
                    }

                    output += "</tr></thead><tbody>";
                }
                else if (i >= 2)
                {
                    // Rows
                    var cols = rows[i].Split("| ").Where(x => !string.IsNullOrEmpty(x)).ToList();

                    if (cols.Count > maxColumns)
                    {
                        maxColumns = cols.Count; // Update the maximum column count if a row has more columns
                    }

                    rowList.Add(cols); // Add the row to the list
                }
            }

            // Generate the table rows
            foreach (var row in rowList)
            {
                output += "<tr>";

                for (var i = 0; i < maxColumns; i++)
                {
                    var col = i < row.Count ? row[i].Trim() : ""; // Use an empty string for missing cells
                    output += $"<td>{col}</td>";
                }

                output += "</tr>";
            }

            output += "</tbody></table>";
            return output;
        }
    }
}