using AppelsDOffresApp.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppelsDOffresApp.Models
{
    public class Documents
    {
        [Key]
        public int Id { get; set; }

        [Required] // Ajouter une validation pour s'assurer que le nom est toujours rempli
        public string Nom { get; set; }

        [Required] 
        public DocumentType Type { get; set; }

        [Required] 
        public string FilePath { get; set; }

        [ForeignKey("AppelOffre")]
        public int AppelDOffreId { get; set; }

        // Navigation property
        public virtual AppelOffre AppelOffre { get; set; }
        public DateTime DateLimite { get; set; }
    }
}
