// File: Controllers/UtilisateurController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppelsDOffresApp.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace AppelsDOffresApp.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly AppDbContext _context;

        public UtilisateurController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> MyTenders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tenders = await _context.AppelsDOffres
                            .Where(a => a.AssignedUserId == userId)
                            .ToListAsync();

            return View(tenders);
        }
    }
}
