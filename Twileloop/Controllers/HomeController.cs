using Microsoft.AspNetCore.Mvc;
using Serilog;

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
        [Route("read")]
        public async Task<IActionResult> Read()
        {
            Log.Information("Visited {@Page}", "Index");
            return View();
        }
    }
}