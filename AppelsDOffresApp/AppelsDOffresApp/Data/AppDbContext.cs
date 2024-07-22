using AppelsDOffresApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace AppelsDOffresApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<AppelOffre> AppelsDOffres { get; set; }
        public DbSet<Documents> Documents { get; set; }
       // public DbSet<Rappel> Rappels { get; set; }
        //public DbSet<AppelDOffreCollaborateur> AppelDOffreCollaborateurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* base.OnModelCreating(modelBuilder);

            // Configuration pour AppelOffre et Utilisateur relation plusieurs-à-plusieurs
            modelBuilder.Entity<AppelDOffreCollaborateur>()
                .HasKey(ac => new { ac.AppelDOffreId, ac.UtilisateurId });

            modelBuilder.Entity<AppelDOffreCollaborateur>()
                .HasOne(ac => ac.AppelDOffre)
                .WithMany(a => a.Collaborateurs)
                .HasForeignKey(ac => ac.AppelDOffreId);

            modelBuilder.Entity<AppelDOffreCollaborateur>()
                .HasOne(ac => ac.Utilisateur)
                .WithMany()
                .HasForeignKey(ac => ac.UtilisateurId);
            
            // Configuration pour la relation AppelDOffre -> Utilisateur (Gestionnaire)
            modelBuilder.Entity<AppelOffre>()
                .HasOne(a => a.Gestionnaire)
                .WithMany()
                .HasForeignKey(a => a.GestionnaireId)
                .OnDelete(DeleteBehavior.Restrict);*/
        }
    }
}
