using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppelsDOffresApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

public class AppDbContext : IdentityDbContext<Utilisateur>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<AppelOffre> AppelsDOffres { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Documents> Documents { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure the relationship between ChatMessage and Utilisateur for the sender
        builder.Entity<ChatMessage>()
            .HasOne<Utilisateur>(cm => cm.FromUser)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(cm => cm.FromUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship between ChatMessage and Utilisateur for the recipient
        builder.Entity<ChatMessage>()
            .HasOne<Utilisateur>(cm => cm.ToUser)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(cm => cm.ToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AppelOffre>()
           .HasOne(a => a.AssignedUser)
           .WithMany(u => u.AppelsDOffres)
           .HasForeignKey(a => a.AssignedUserId)
           .OnDelete(DeleteBehavior.SetNull); 
    }


}
