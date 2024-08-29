using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppelsDOffresApp.Models
{
    public class Utilisateur : IdentityUser
    {
        [StringLength(100)]
        public string? Nom { get; set; }

        // Navigation properties
        public virtual ICollection<AppelOffre> AppelsDOffresGestionnaire { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; }
        public string? ConnectionId { get; set; }
        // Navigation properties for chat messages
        public ICollection<ChatMessage> SentMessages { get; set; }
        public ICollection<ChatMessage> ReceivedMessages { get; set; }
        public ICollection<AppelOffre> AppelsDOffres { get; set; }
        public Utilisateur()
        {
            AppelsDOffresGestionnaire = new HashSet<AppelOffre>();
            AppelsDOffres = new HashSet<AppelOffre>();
            Messages = new HashSet<ChatMessage>();
        }
    }
}
