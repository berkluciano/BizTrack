using Microsoft.AspNetCore.Mvc;

namespace UniProject1.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
