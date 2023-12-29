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
        [Route("sangeeth.nandakumar")]
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
        public async Task<IActionResult> Error([FromQuery] string referer)
        {
            var model = new ErrorVM
            {
                Referer = referer,
            };
            return View(model);
        }

        [HttpGet]
        [Route("redirect")]
        public async Task<IActionResult> Redirect([FromQuery] string from, [FromQuery] string to)
        {
            var model = new Redirect
            {
                RedirectFrom = from,
                RedirectTo = to,
                Event = DateTime.UtcNow
            };           
            return View(model);
        }
    }
}