using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AppelsDOffresApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AppelsDOffresApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppelsDOffresApp.Controllers
{
    [Authorize]
    public class AppelOffreController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AppelOffreController> _logger;
        private readonly UserManager<Utilisateur> _userManager;

        public AppelOffreController(AppDbContext context, ILogger<AppelOffreController> logger, UserManager<Utilisateur> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: AppelOffre
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppelsDOffres.ToListAsync());
        }

        // GET: AppelOffre/Create
        [Authorize(Roles = "Admin,Gestionnaire")]
        // GET: Create
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppelOffreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appelOffre = new AppelOffre
                {
                    Numero = model.Numero,
                    Nom = model.Nom,
                    Date = model.Date,
                    Description = model.Description,
                    Status = model.Status,
                    AssignedUserId = model.AssignedUserId // Nullable
                };

                _context.Add(appelOffre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Users = new SelectList(_context.Users, "Id", "UserName", model.AssignedUserId);
            return View(model);
        }





        // GET: AppelOffre/Edit/5
        [Authorize(Roles = "Admin,Gestionnaire")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appelOffre = await _context.AppelsDOffres.FindAsync(id);
            if (appelOffre == null)
            {
                return NotFound();
            }
            return View(appelOffre);
        }

        // POST: AppelOffre/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin,Gestionnaire")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Nom,Date,Description,Status")] AppelOffre appelOffre)
        {
            if (id != appelOffre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appelOffre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppelOffreExists(appelOffre.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appelOffre);
        }

        // GET: AppelOffre/Delete/5
        [Authorize(Roles = "Admin,Gestionnaire")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appelOffre = await _context.AppelsDOffres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appelOffre == null)
            {
                return NotFound();
            }

            return View(appelOffre);
        }

        // POST: AppelOffre/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Gestionnaire")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appelOffre = await _context.AppelsDOffres.FindAsync(id);
            _context.AppelsDOffres.Remove(appelOffre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppelOffreExists(int id)
        {
            return _context.AppelsDOffres.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Admin,Gestionnaire")]
        // GET: AppelOffre/AssignTenders
        public async Task<IActionResult> AssignTenders()
        {
            var tenders = await _context.AppelsDOffres.Include(a => a.AssignedUser).ToListAsync();
            var users = await _userManager.Users.ToListAsync();

            var viewModel = new AssignTendersViewModel
            {
                AppelsDOffres = tenders,
                Users = users
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AssignTender(int tenderId, string assignedUserId)
        {
            var tender = await _context.AppelsDOffres.FindAsync(tenderId);
            if (tender != null)
            {
                tender.AssignedUserId = assignedUserId;
                _context.Update(tender);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AssignTenders));
        }

    }
}
