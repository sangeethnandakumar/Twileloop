using Microsoft.AspNetCore.Mvc;

namespace Twileloop.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        [Route("pricing")]
        public IActionResult Pricing() {
            return View();
        }
    }
}