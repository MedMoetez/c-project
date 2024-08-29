using AppelsDOffresApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppelsDOffresApp.Models
{
    public class AppelOffre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)] // La longueur maximale du numéro est de 100 caractères
        public string Numero { get; set; }

        [Required]
        [StringLength(200)] // La longueur maximale du nom est de 200 caractères
        public string Nom { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(1000)] // La longueur maximale de la description est de 1000 caractères
        public string Description { get; set; }

        [Required]
        public AppelOffreStatus Status { get; set; }

        // Collection de documents associée à cet Appel d'Offre
        public virtual ICollection<Documents> Documents { get; set; }
        public string? AssignedUserId { get; set; }
        public Utilisateur? AssignedUser { get; set; }



        public AppelOffre()
        {
            Documents = new HashSet<Documents>();
        }
    }
}
