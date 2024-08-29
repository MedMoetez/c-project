// File: ViewModels/AppelOffreViewModel.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace AppelsDOffresApp.ViewModels
{
    public class AppelOffreViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public AppelOffreStatus Status { get; set; }

        public string? AssignedUserId { get; set; }
    }
}
