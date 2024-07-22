using System.Data;

namespace AppelsDOffresApp.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public Data.Role Role { get; set; }

        // Navigation properties
        public virtual ICollection<AppelOffre> AppelsDOffresGestionnaire { get; set; }
        public virtual ICollection<AppelOffre> AppelsDOffresCollaborateur { get; set; }
        public virtual ICollection<Rappel> Rappels { get; set; }
    }
}
