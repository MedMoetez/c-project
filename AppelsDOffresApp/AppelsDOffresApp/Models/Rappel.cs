namespace AppelsDOffresApp.Models;
public class Rappel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public int UtilisateurId { get; set; }

    // Navigation property
    public virtual Utilisateur Utilisateur { get; set; }
}
