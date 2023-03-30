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

        [Route("features")]
        public IActionResult Features() {
            return View();
        }

        [Route("pricing")]
        public IActionResult Pricing() {
            return View();
        }

        [Route("about")]
        public IActionResult About() {
            return View();
        }
    }
}