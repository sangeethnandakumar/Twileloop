using Microsoft.AspNetCore.Mvc;
using Serilog;
using Twileloop.UOW;

namespace Packages.Twileloop.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Log.Information("Visited {@Page}", "Index");
            return View();
        }

        [HttpGet]
        [Route("portfolio")]
        public async Task<IActionResult> Portfolio()
        {
            Log.Information("Visited {@Page}", "Index");
            return View();
        }

        [HttpGet]
        [Route("blogs/{slug}")]
        public async Task<IActionResult> Blogs([FromRoute] string slug)
        {
            Log.Information("Visited {@Page}", "Index");
            return View();
        }
    }
}