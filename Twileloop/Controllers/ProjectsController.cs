using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text;
using Twileloop.Helpers;
using Twileloop.Models;
using Twileloop.UOW.MongoDB.Core;

namespace Packages.Twileloop.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly UnitOfWork uow;
        private readonly Repository<Project> projectRepo;

        public ProjectsController(UnitOfWork uow)
        {
            this.uow = uow;
            this.projectRepo = uow.GetRepository<Project>();
        }

        [HttpGet]
        [Route("projects/home")]
        public async Task<IActionResult> Index([FromQuery] string anchor = null)
        {
            if (anchor is not null)
            {
                try
                {
                    var clientIp = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    var decodedIntegrity = Encoding.UTF8.GetString(Convert.FromBase64String(anchor));
                    var ipDetails = await IPTracker.GetIPInfoAsync(clientIp);
                    Log.Fatal("{@User}: visited '{@Page}' from {@IP}", decodedIntegrity, "Home", ipDetails);
                }
                catch
                {
                }
            }
            var allProjects = projectRepo.GetAll().ToList();
            return View("home", allProjects);
        }

        [HttpGet]
        [Route("projects/{slug}")]
        public async Task<IActionResult> Projects([FromRoute] string slug, [FromQuery] string anchor = null)
        {
            if (anchor is not null)
            {
                try
                {
                    var clientIp = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    var decodedIntegrity = Encoding.UTF8.GetString(Convert.FromBase64String(anchor));
                    var ipDetails = await IPTracker.GetIPInfoAsync(clientIp);
                    Log.Fatal("{@User}: visited '{@Page}' from {@IP}", decodedIntegrity, slug, ipDetails);
                }
                catch
                {
                }
            }
            var project = projectRepo.Find(x => x.Slug == slug).FirstOrDefault();
            return View("Project", project);
        }
    }
}