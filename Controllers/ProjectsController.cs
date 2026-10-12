using Microsoft.AspNetCore.Mvc;

namespace UniProject1.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
