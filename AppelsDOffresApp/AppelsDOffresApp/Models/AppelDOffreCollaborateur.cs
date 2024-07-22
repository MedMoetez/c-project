namespace AppelsDOffresApp.Models
{
    public class AppelDOffreCollaborateur
    {
        public int AppelDOffreId { get; set; }
        public virtual AppelOffre AppelDOffre { get; set; }

        public int UtilisateurId { get; set; }
        public virtual Utilisateur Utilisateur { get; set;}
    }
}
