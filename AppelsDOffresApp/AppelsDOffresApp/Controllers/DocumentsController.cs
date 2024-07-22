using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppelsDOffresApp.Data;
using AppelsDOffresApp.Models;

namespace AppelsDOffresApp.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly AppDbContext _context;

        public DocumentsController(AppDbContext context)
        {
            _context = context;
        }

        // Afficher les documents
        public async Task<IActionResult> Index()
        {
            var documents = await _context.Documents.Include(d => d.AppelOffre).ToListAsync();
            return View(documents);
        }

        // Afficher les appels d'offres en cours pour ajouter des documents
        public async Task<IActionResult> AddToAppelOffre()
        {
            var appelsOffresEnCours = await _context.AppelsDOffres
                                                   .Where(ao => ao.Status == AppelOffreStatus.EnCours)
                                                   .ToListAsync();
            return View(appelsOffresEnCours);
        }

        // Afficher le formulaire pour ajouter un document à un appel d'offre spécifique
        public IActionResult Create(int appelOffreId)
        {
            ViewBag.AppelOffreId = appelOffreId;
            return View();
        }

        // Ajouter un document à un appel d'offre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Documents document)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await document.UploadedFile.CopyToAsync(memoryStream);
                    document.Contenu = memoryStream.ToArray();
                }

                _context.Documents.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(document);
        }
    }
}
