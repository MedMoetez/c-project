using AppelsDOffresApp.Data;
using AppelsDOffresApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppelsDOffresApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly Data.AppDbContext _context;

        public HomeController(Data.AppDbContext context)
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
