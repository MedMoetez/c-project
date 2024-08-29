using AppelsDOffresApp.ViewModels;
using AppelsDOffresApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
namespace AppelsDOffresApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
             var model = new HomeViewModel
        {
            ActiveTendersCount = _context.AppelsDOffres.Count(a => a.Status == AppelOffreStatus.EnCours),
            DocumentsSentCount = _context.Documents.Count(),
            //PendingRemindersCount = _context.Rappels.Count(r => r.Date > DateTime.Now)
        };
        
        return View(model);
    }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
