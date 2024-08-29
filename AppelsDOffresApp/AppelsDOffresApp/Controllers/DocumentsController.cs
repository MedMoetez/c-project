using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppelsDOffresApp.Models;
using AppelsDOffresApp.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting.Internal;

namespace AppelsDOffresApp.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DocumentsController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _environment;

        public DocumentsController(AppDbContext context, ILogger<DocumentsController> logger, IWebHostEnvironment environment, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _environment = environment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var documents = await _context.Documents.Include(d => d.AppelOffre).ToListAsync();
            return View(documents);
        }

        [Authorize(Roles = "Admin,Collaborateur,Gestionnaire")]
        public async Task<IActionResult> AddToAppelOffre()
        {
            var appelsOffresEnCours = await _context.AppelsDOffres
                                                   .Where(ao => ao.Status == AppelOffreStatus.EnCours)
                                                   .ToListAsync();
            return View(appelsOffresEnCours);
        }

        [Authorize(Roles = "Admin,Collaborateur,Gestionnaire")]
        public IActionResult Create(int appelOffreId)
        {
            ViewBag.AppelOffreId = appelOffreId;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Collaborateur,Gestionnaire")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentViewModel viewModel)
        {
            _logger.LogInformation("Entered Create method.");

            if (ModelState.IsValid)
            {
                var document = new Documents
                {
                    Nom = viewModel.Nom,
                    Type = viewModel.Type,
                    AppelDOffreId = viewModel.AppelDOffreId,
                    DateLimite = viewModel.DateLimite // Save document deadline
                };

                if (viewModel.UploadedFile != null && viewModel.UploadedFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);
                    var filePath = Path.Combine(uploadsFolder, viewModel.UploadedFile.FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.UploadedFile.CopyToAsync(fileStream);
                    }

                    document.FilePath = Path.Combine("uploads", viewModel.UploadedFile.FileName);
                    _context.Documents.Add(document);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("No file was uploaded.");
                    ModelState.AddModelError("", "Please upload a file.");
                }
            }

            _logger.LogWarning("ModelState is not valid.");
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    _logger.LogWarning(error.ErrorMessage);
                }
            }

            return View(viewModel);
        }


        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.AppelOffre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                // Delete the physical file
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, document.FilePath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Remove the document from the database
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Document with ID {DocumentId} not found.", id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }



        [AllowAnonymous]
        public async Task<IActionResult> Download(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(_environment.WebRootPath, document.FilePath);
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));
        }
    }
}
