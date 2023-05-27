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
    }
}