using Microsoft.AspNetCore.Mvc;

namespace AppelsDOffresApp.Controllers
{
    public class RappelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
