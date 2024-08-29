using AppelsDOffresApp.Models;
using System.ComponentModel.DataAnnotations;

namespace AppelsDOffresApp.ViewModels
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public DocumentType Type { get; set; }
        public IFormFile UploadedFile { get; set; } 
        public int AppelDOffreId { get; set; }
        public DateTime DateLimite { get; set; }
    }

}
