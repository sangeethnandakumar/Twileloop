using Microsoft.AspNetCore.Mvc;
using Packages.Twileloop.Models;
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

        [HttpGet]
        [Route("error")]
        public async Task<IActionResult> Error([FromQuery]string referer)
        {
            var errorVM = new ErrorVM
            {
                Referer = referer,
            };
            return View(errorVM);
        }
    }
}