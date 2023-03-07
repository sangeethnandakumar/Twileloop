using Microsoft.AspNetCore.Mvc;

namespace Twileloop.Controllers {
    public class AssetsController : Controller {

        [HttpGet("/assets/social/{app}")]
        public IActionResult Social([FromRoute] string app) {
            if(string.IsNullOrEmpty(app)) {
                return Redirect("https://twileloop.com/" + "images/products/index.png");
            }
            else {
            return Redirect("https://twileloop.com/" + "images/products/" + app + ".png");
            }
        }
    }
}