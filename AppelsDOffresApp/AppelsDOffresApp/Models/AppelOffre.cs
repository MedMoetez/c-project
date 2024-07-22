using AppelsDOffresApp.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppelsDOffresApp.Models
{
    public class AppelOffre
    {
        [Key]
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Nom { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public AppelOffreStatus Status { get; set; }

        // Navigation properties
        public virtual ICollection<Documents> Documents { get; set; }

        public AppelOffre()
        {
            Documents = new HashSet<Documents>();
        }

       // public virtual ICollection<Documents> Documents { get; set; }

        /* public int? GestionnaireId { get; set; } // Rendre nullable
         [ForeignKey("GestionnaireId")]
         // Navigation properties
         public virtual Utilisateur Gestionnaire { get; set; }

         public virtual ICollection<AppelDOffreCollaborateur> Collaborateurs { get; set; }

         public string DescriptionOffre { get; set; }*/

    }
}
