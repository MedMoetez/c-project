using AppelsDOffresApp.Data;
using AppelsDOffresApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AppelsDOffresApp.Controllers
{
    public class AppelOffreController : Controller
    {
        private readonly AppDbContext _context;

        public AppelOffreController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AppelOffre
        public async Task<IActionResult> Index()
        {
            var appelsOffres = await _context.AppelsDOffres.ToListAsync();
            return View(appelsOffres);
        }

        // GET: AppelOffre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppelOffre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppelOffre appelOffre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appelOffre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appelOffre);
        }

        // GET: AppelOffre/Edit/5
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppelOffre appelOffre)
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appelOffre = await _context.AppelsDOffres.FindAsync(id);
            if (appelOffre != null)
            {
                _context.AppelsDOffres.Remove(appelOffre);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AppelOffreExists(int id)
        {
            return _context.AppelsDOffres.Any(e => e.Id == id);
        }
    }
}
